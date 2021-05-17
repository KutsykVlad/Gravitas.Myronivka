$(function () {
	var w = 300;
	var h = 400;
	var padding = 2;

	var monthlySales = [
		{ "month": 10, "sales": 100 },
		{ "month": 20, "sales": 130 },
		{ "month": 30, "sales": 250 },
		{ "month": 40, "sales": 300 },
		{ "month": 50, "sales": 265 },
		{ "month": 60, "sales": 225 },
		{ "month": 70, "sales": 180 },
		{ "month": 80, "sales": 120 },
		{ "month": 90, "sales": 145 },
		{ "month": 100, "sales": 130 }
	];

	var lineFn = d3.line()
		.x(function (d) { return d.month * 2; })
		.y(function (d) { return h - d.sales; })
		.curve(d3.curveLinear);

	var svg = d3.select('#preview2')
		.append('svg')
		.attrs({
			width: w,
			height: h
		});

	var viz = svg.append('path')
		.attrs({
			d: lineFn(monthlySales),
			'stroke': 'red',
			'stroke-width': 2,
			'fill': 'none'
		});

	var labels = svg.selectAll('text')
		.data(monthlySales)
		.enter()
		.append('text')
		.text(function(d) { return d.sales; })
		.attrs({
			x: function(d) { return (d.month * 3)-25; },
			y: function(d) { return h - d.sales; },
			'font-size': '12px',
			'font-family': 'sans-serif',
			'fill': '#444444',
			'text-anchor': 'start',
			'dy': '.35em',
			'font-weight': function(d, i) {
				if (i === 0 || i === (monthlySales.length - 1)) {
					return 'bold';
				} else {
					return 'normal';
				}
			}
		});

});
