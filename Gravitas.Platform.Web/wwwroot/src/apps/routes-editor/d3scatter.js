$(function () {
	var w = 600;
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

	function salesKPI(d) {
		if (d >= 250) {
			return '#33cc66';
		} else if (d < 250) {
			return '#ccc';
		}
	}

	function showMinMax(ds, col, val, type) {
		var max = d3.max(ds, function(d) { return d[col]; });
		var min = d3.min(ds, function (d) { return d[col]; });

		if (type === 'minmax' && (val === max || val === min)) {
			return val;
		} else {
			if (type === 'all') {
				return val;
			}
		}
	}

	var svg = d3.select('#preview3')
		.append('svg')
		.attrs({
			width: w,
			height: h
		});

	var dots = svg.selectAll('circle')
		.data(monthlySales)
		.enter()
		.append('circle')
		.attrs({
			cx: function(d) { return d.month * 3; },
			cy: function(d) { return h - d.sales; },
			r: 5,
			'fill': function(d) { return salesKPI(d.sales); }
		});

	var labels = svg.selectAll('text')
		.data(monthlySales)
		.enter()
		.append('text')
		.text(function (d) { return showMinMax(monthlySales, 'sales', d.sales, 'minmax'); })
		.attrs({
			x: function (d) { return (d.month * 3) - 25; },
			y: function (d) { return h - d.sales; },
			'font-size': '12px',
			'font-family': 'sans-serif',
			'fill': '#444444',
			'text-anchor': 'start'
		});
});
