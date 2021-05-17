jQuery(function($) {
	var appRoot = $('#dropdownConstructorRoot');
	var store = $('#dropdownConstructorStore');

	var group = JSON.parse(appRoot.attr('data-group'));
	var node = JSON.parse(appRoot.attr('data-node'));
	var currentConfig = JSON.parse(store.val());
	var lineNumber = 0;

	var placeHolder = appRoot.attr('data-placeholder');

	function createDropdown(items, selectedId) {
		var dropdownHtml = '<div class="col-xs-3"><select class="form-control" id="'+lineNumber+'">';

		dropdownHtml += '<option>'+placeHolder+'</option>';
		items.forEach(function (item) {
			var isSelected = selectedId === item.id;

			if (isSelected) {
				dropdownHtml += '<option selected data-id="'+item.id+'">'+item.name+'</option>';
			} else {
				dropdownHtml += '<option data-id="'+item.id+'">'+item.name+'</option>';
			}

		});

		dropdownHtml += '</select></div>';

		return dropdownHtml;
	}

	function createLine (content, isActive, id) {
		id = id || ++lineNumber;
		var lineHtml = '<div data-line-id="' + id + '" class="row mt-2 newLine"';

		lineHtml += isActive ? '>' : ' style="pointer-events: none;">';
		lineHtml += content;
		lineHtml += '</div>';

		return lineHtml;
	}

	function getChidDropdownNodes (parentId, exceptIds) {
		exceptIds = exceptIds || [];

		return node.items.filter(function (item) {
			return +parentId === +item.nodeGroupId && !exceptIds.includes(item.id);
		});
	}

	function renderApp () {
		var startContent = initStartConfig();

		if (!currentConfig.disableAppend) startContent += createLine(createDropdown(group.items), true);
		appRoot.html(startContent);
	}

	function initStartConfig () {
		var resultHTML = '';

		for (var key in currentConfig.groupDictionary) {
			if (currentConfig.groupDictionary[key].groupId) {
				var lineContent = createDropdown(group.items, currentConfig.groupDictionary[key].groupId);
				var exceptIds = [];

				lineContent += '<div class="col-xs-9 nodeItems">';
				if (currentConfig.groupDictionary[key].nodeList) {
					currentConfig.groupDictionary[key].nodeList.forEach(function (pathItem) {
						exceptIds.push(pathItem.id);
					});

					currentConfig.groupDictionary[key].nodeList.forEach(function (pathItem) {
						var exceptIdsCopy = exceptIds.filter(function (value) {
							return value !== pathItem.id;
						});
						lineContent += createDropdown(getChidDropdownNodes(currentConfig.groupDictionary[key].groupId, exceptIdsCopy), pathItem.id);
					});
				}

				var childGroupNodes = getChidDropdownNodes (currentConfig.groupDictionary[key].groupId, exceptIds);
				if (childGroupNodes.length) {
					lineContent += createDropdown(childGroupNodes);
				}
				lineContent += '</div>';

				resultHTML += createLine(lineContent, currentConfig.groupDictionary[key].active, key);
				lineNumber = key
			}
		}

		return resultHTML;
	}

	function updateStore (selectedId, lineId, dropdownNumber, isNodeItem) {
        if (selectedId === undefined && !isNodeItem) {
            delete currentConfig.groupDictionary[lineId];

            var temp = {};
            var i = 1;
			for (var key in currentConfig.groupDictionary){
				temp[i] = currentConfig.groupDictionary[key];
				i++;
			}
			currentConfig.groupDictionary = temp;
			
            lineNumber = 0;
            store.val(JSON.stringify(currentConfig)).trigger('storeUpdated');
            return;
        }

		if (selectedId === undefined && isNodeItem) {
			currentConfig.groupDictionary[lineId].nodeList.splice(dropdownNumber, 1);
			store.val(JSON.stringify(currentConfig)).trigger('storeUpdated');
			return;
		}

		if (!currentConfig.groupDictionary[lineId]) {
			currentConfig.groupDictionary[lineId] = {};
		}

		if (!isNodeItem) {
			currentConfig.groupDictionary[lineId].groupId = +selectedId;
			currentConfig.groupDictionary[lineId].nodeList = [];
			currentConfig.groupDictionary[lineId].active = true;
			currentConfig.groupDictionary[lineId].quotaEnabled = true;
		} else {
			if (!currentConfig.groupDictionary[lineId].nodeList) {
				currentConfig.groupDictionary[lineId].nodeList = [];
			}

			var fullNode = {};
			node.items.forEach(function (node) {
				if (node.id == selectedId) {
					fullNode = node;
				}
			});

			if (jQuery.isEmptyObject(fullNode)) {
				currentConfig.groupDictionary[lineId].nodeList.length = dropdownNumber;
			} else {
				currentConfig.groupDictionary[lineId].nodeList[dropdownNumber] = fullNode;
			}
		}

		lineNumber = 0;
		store.val(JSON.stringify(currentConfig)).trigger('storeUpdated');
	}

	appRoot.on('change', 'select', function () {
		var lineId = $(this).closest('.newLine').attr('data-line-id');
		var selectedId = $(this).find('option:selected').attr('data-id');
		var dropdownNumberInLine = $(this).parent().index();
		var isNodeItem = $(this).parent().parent().hasClass('nodeItems');

		updateStore (selectedId, lineId, dropdownNumberInLine, isNodeItem);
	});

	store.on('storeUpdated', function () {
		renderApp();
	});

	renderApp();
});
