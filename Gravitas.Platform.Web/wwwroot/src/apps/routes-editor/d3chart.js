$(function() {
	var w = 300;
	var h = 100;
	var padding = 2;
	var dataset = [5, 10, 13, 19, 21, 25, 11, 23, 22, 18, 7];
	var svg = d3.select("#preview1").append('svg').attrs({
		width: w,
		height: h
	});

	function colorPicker(v) {
		if (v <= 20) {
			return "#555555";
		} else if (v > 20) {
			return '#ff0033';
		}
	};

svg.selectAll('rect')
	.data(dataset)
	.enter()
	.append('rect')
.attrs({
	x: function (d, i) { return i * (w / dataset.length); },
	y: function (d) { return h - (d * 4); },
	width: w / dataset.length - padding,
	height: function (d) { return d * 4; },
	fill: function(d) { return colorPicker(d); }
		});

	svg.selectAll("text")
		.data(dataset)
		.enter()
		.append("text")
		.text(function(d) { return d; })
		.attrs({
			'text-anchor': 'middle',
			x: function(d, i) { return i* ((w*1.0) / dataset.length) +((w*1.0)/dataset.length - padding)/2.0; },
			y: function (d) { return h - (d * 4) + 14; },
			'font-family': 'sans-serif',
			'font-size': 12,
			'fill': '#ffffff'
		});
});
