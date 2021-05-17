$(function () {
	function sum(arr) {
		// returns the sum total of all values in the array
		return _.reduce(arr, function (memo, num) { return memo + num }, 0);
	}

	// Create a new directed graph
	var g = new dagreD3.graphlib.Graph().setGraph({}).setDefaultEdgeLabel(function () { return {}; });

	var fullUrl = document.URL; // Get current url
	var urlArray = fullUrl.split('/');
	var id = urlArray[urlArray.length - 1].split('#')[0];

	var options = {
		url: 'http://localhost:3142/api/Routes/GetRoute',
		type: 'GET',
		dataType: 'json',
		contentType: 'application/json; charset=utf-8',
		data: {'id': id}
	};

	$.ajax(options).done(function (response) {
		console.log('d3');
		//console.log(response);
		var route = JSON.parse(response.RouteConfig);
		var nodes = route.Nodes;

		var shifts = [];
		var index = 0;
		var shift = 0;
		_.each(nodes, function (node) {
			index = node.Position + sum(shifts);

			g.setNode(index,
			{
				label: '<h3 style="color:black;">' + node.GroupName + '</h3><hr/>',
				class: 'group-node',
				labelType: 'html'
			});
			
			if (node.NodeDots) {
				var k = 1;
				shift = node.NodeDots.length;
				shifts.push(shift);

				_.each(node.NodeDots, function (dot) {
					g.setNode(index + k,
						{
							label: '<div class="alert alert-info">' + '#'+dot.Id + ' - ' + dot.Name + '</div>',
							class: 'node',
							labelType: 'html'
						});
					g.setEdge(index, index + k);
					k++;
				});
			}

			//index = index + shift;
			//g.setEdge(node.Position, index);
		});

		g.setNode(index + shift+1,
			{
				label: '<div class="alert alert-info">END</div>',
				class: 'node',
				labelType: 'html'
			});

		shifts = [];
		index = 0;
		shift = 0;
		_.each(nodes, function (node) {
			index = node.Position + sum(shifts);

			if (node.NodeDots) {
				var k = 1;
				shift = node.NodeDots.length;
				shifts.push(shift);

				_.each(node.NodeDots, function (dot) {
					g.setEdge(index + k, node.Position + sum(shifts)+1);
					k++;
				});
			}
			//g.setEdge(index, index + shift);
		});


		g.nodes().forEach(function (v) {
			var node = g.node(v);
			// Round the corners of the nodes
			node.rx = node.ry = 5;
		});

		// Set up edges, no special attributes.
		//for (var i = 0; i < nodes.length-1; i++) {
		//	g.setEdge(nodes[i].Position, nodes[i+1].Position);
		//}
		//g.setEdge(2, 3);
		//g.setEdge(3, 4);
		//g.setEdge(5, 6);
		//g.setEdge(9, 10);
		//g.setEdge(8, 9);
		//g.setEdge(11, 12);
		//g.setEdge(8, 11);
		//g.setEdge(5, 8);
		//g.setEdge(1, 5);
		//g.setEdge(13, 14);
		//g.setEdge(1, 13);
		//g.setEdge(0, 1);

		var svg = d3.select("#preview4 svg");


		var inner = svg.select("g");
		// Set up zoom support
		var zoom = d3.zoom().on("zoom", function () {
			inner.attr("transform", d3.event.transform);
		});
		svg.call(zoom);
		// Create the renderer
		var render = new dagreD3.render();
		// Run the renderer. This is what draws the final graph.
		render(inner, g);
		// Center the graph
		var initialScale = 1.0;
		svg.call(zoom.transform, d3.zoomIdentity.translate((svg.attr("width") - g.graph().width * initialScale) / 2, 20).scale(initialScale));
		svg.attr('height', 1000);// g.graph().height * initialScale + 40

		// Center the graph
		//var xCenterOffset = (svg.attr("width") - g.graph().width) / 2;
		//inner.attr("transform", "translate(" + xCenterOffset + ", 20)");
		//svg.attr("height", g.graph().height + 40);
	}).fail(function (error, textStatus) {
	});

	
	

	


	

});
