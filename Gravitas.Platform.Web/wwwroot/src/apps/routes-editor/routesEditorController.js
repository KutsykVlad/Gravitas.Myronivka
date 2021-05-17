
function routesEditorController($scope, $uibModal, $timeout, routesService, $location, $window, toastr, $interval, $http) {
	$scope.loading = true;
	$scope.routeId = $location.path().split(/[\s/]+/).pop();
	$scope.closeOptions = false;

	$scope.oneAtATime = true;
	$scope.groups = [];
	$scope.route = {};

	$scope.route.Config = {
		Nodes: []
	};

	routesService.getRoute($scope.routeId).then(function (response) {
		$timeout(function () {
			$scope.loading = false;
			$scope.route = angular.fromJson(response.data);
			$scope.route.Config = angular.fromJson($scope.route.RouteConfig);
			$scope.showDrop = false;

			if (!$scope.route.Config) {
				$scope.route.Config = {
					Nodes: []
				};
				$scope.showDrop = true;
			}
			
			if ($scope.route.Config.Nodes.length !== 0) {
				$scope.showDrop = false;
				_.each($scope.route.Config.Nodes, function (g) {
					switch (g.RoutineGroupId) {
					case 1:
						g.Icon = 'fa-windows';
						break;
					case 3:
						g.Icon = 'fa-sign-in';
						break;
					case 4:
						g.Icon = 'fa-sign-out';
						break;
					case 5:
						g.Icon = 'fa-flag-checkered';
						break;
					case 6:
						g.Icon = 'fa-flask';
						break;
					case 7:
						g.Icon = 'fa-thermometer';
						break;
					case 8:
						g.Icon = 'fa-chain-broken';
						break;
					case 9:
						g.Icon = 'fa-cubes';
						break;
					case 10:
						g.Icon = 'fa-wheelchair';
						break;
					default:
						g.Icon = 'fa-question';

					}
					if (!g.Description) {
						g.Description = 'No Description';
					}

				});
					
			}

			console.log($scope.route.Config.Nodes);
		}, 400);
	});

	routesService.getNodes().then(function (response) {
		if (response.data) {
			$scope.nodes = angular.fromJson(response.data);
		}
	});

	routesService.getRoutines().then(function (response) {
		$timeout(function () {
			$scope.loading = false;
			console.log(response.data);
			$scope.groups = angular.fromJson(response.data);
			_.each($scope.groups, function (g) {
				switch (g.Id) {
				case 1:
						g.Icon = 'fa-windows';
						break;
					case 3:
						g.Icon = 'fa-sign-in';
						break;
				case 4:
					g.Icon = 'fa-sign-out';
						break;
				case 5:
						g.Icon = 'fa-flag-checkered';
						break;
				case 6:
					g.Icon = 'fa-flask';
						break;
				case 7:
						g.Icon = 'fa-thermometer';
						break;
				case 8:
					g.Icon = 'fa-chain-broken';
						break;
				case 9:
					g.Icon = 'fa-cubes';
					break;
				case 10:
						g.Icon = 'fa-wheelchair';
					break;
				default:
						g.Icon = 'fa-question';

				}
				if (!g.Description) {
					g.Description = 'No Description';
				}

			});
		}, 400);
	});

	$scope.updateRoute = function () {
		$scope.loading = true;
		routesService.getRoute($scope.routeId).then(function (response) {
			$scope.route = angular.fromJson(response.data);
			$scope.route.Config = angular.fromJson($scope.route.RouteConfig);

				_.each($scope.route.Config.Nodes, function (g) {
					switch (g.RoutineGroupId) {
					case 1:
						g.Icon = 'fa-windows';
						break;
					case 3:
						g.Icon = 'fa-sign-in';
						break;
					case 4:
						g.Icon = 'fa-sign-out';
						break;
					case 5:
						g.Icon = 'fa-flag-checkered';
						break;
					case 6:
						g.Icon = 'fa-flask';
						break;
					case 7:
						g.Icon = 'fa-thermometer';
						break;
					case 8:
						g.Icon = 'fa-chain-broken';
						break;
					case 9:
						g.Icon = 'fa-cubes';
						break;
					case 10:
						g.Icon = 'fa-wheelchair';
						break;
					default:
						g.Icon = 'fa-question';

					}
					if (!g.Description) {
						g.Description = 'No Description';
					}

			});

			$scope.loading = false;
			_.each($scope.route.Config.Nodes, function (q) {
				q.selected = false;
			});
		},
			function (error) {
				//	$scope.loading = false;
				if (error && error.data && error.data.Message) {
					toastr.error('Can\'t update the route', 'Error');
				}
			}
		);
	};


	$scope.sortableOptions = {
		//containment: '#sortable-container',
		//containerPositioning: 'relative',
		allowDuplicates: true,

		orderChanged: function (event) {
			//https://github.com/a5hik/ng-sortable/issues/361
			var items = event.source.sortableScope.modelValue.map(function (a) { return a.Id; });
			var nodesToService = event.source.sortableScope.modelValue.map(function (a) { return a; });
			_.each(nodesToService, function (q) {
				q.selected = false;
			});

			var nodesIdsPos = event.dest.sortableScope.modelValue.map(function(a) {
				return {
					Id: a.Id,
					Position: a.Position
				};
			});

			_.each($scope.route.Config.Nodes, function (q, val) {

				var i = _.find(items, function (item) {
					return q.Id === item;
				});

				var pos = items.indexOf(i) + 1;
				var node = _.find($scope.route.Config.Nodes, function (qu) { return qu.Id === q.Id; });
				var nIndex = $scope.route.Config.Nodes.indexOf(node);
				$scope.route.Config.Nodes[nIndex].Position = pos;
			});

			var postData = {
				'id': $scope.routeId,
				'nodes': nodesIdsPos
			};

			routesService.updateNodesOrder(postData).then(function (response) {
				console.log(angular.fromJson(response.data));
				$scope.updateRoute();
			}, function (error) {
				if (error && error.data && error.data.Message) {
					toastr.error('Can\'t update route', 'Error');
				}
			});
			//console.log('order changed');
		},
		itemMoved: function (item) {
			console.log('ITEM MOVED');
			console.log(item);
		},
		dragStart: function (item) {
			console.log('drag start');
			console.log(item);
		},

		dragEnd: function (item) {
			console.log('drag end');
			console.log(item);
		}
	};

	function uuidv4() {
		return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
			var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
			return v.toString(16);
		});
	}


	$scope.sortableCloneOptions = {
		clone: true,
		accept: function (sourceItemHandleScope, destSortableScope, destItemScope) {
			console.log('accept');
		},
		itemMoved: function (item) {
			var addedNodeTypeIndex = item.source.index;
			var addedNodeId = item.source.itemScope.modelValue.Id;

			_.each($scope.route.Config.Nodes, function (rn) {
				rn.selected = false;
				rn.isRequired = $scope.isRequired(item);
			});

			var groupPostData = {
				'groupId': addedNodeId
			};

			routesService.getGroup(groupPostData).then(function (response) {
				$scope.addedNode = angular.fromJson(response.data);
			});

			var pos = item.dest.index === undefined ? 1 : item.dest.index + 1;
			var postData = {
				'id': $scope.routeId,
				'routineId': addedNodeId,
				'position': pos
			};

			//var newPos = item.dest.index + 1;

			//$scope.route.Config.Nodes.splice(item.dest.index, 0, $scope.addedNode);

			routesService.addNode(postData).then(function (response) {
				$scope.updateRoute();
			}, function (error) {
				if (error && error.data && error.data.Message) {
					toastr.error('Can\'t add node to the route', 'Error');
				}
			});
		}
	};


	$scope.selectNode = function (item) {
		$scope.closeOptions = false;
		_.each($scope.route.Config.Nodes, function (n) {
			n.selected = false;
		});
		var node = _.find($scope.route.Config.Nodes, function (rn) { return rn.Id === item.Id; });
		if (node) {
			node.selected = true;
		}

		// Get available node dots
		var postData = {
			'routeNodeId': node.RoutineGroupId
		};
		routesService.getAvailableNodeDots(postData).then(function (response) {
			if (response.data) {
				node.Dots = angular.fromJson(response.data);

				_.each(node.Dots, function (n) {
					var isSelectedNode = _.find(node.NodeDots, function (nd) { return nd.Id === n.Id; });
					if (isSelectedNode) {
						n.selected = true;
					} else {
						n.selected = false;
					}
				});
				console.log(node.NodeDots);
			} else {
				toastr.error('Can\'t edit node of the route', 'Error');
			}

		}, function(error) {
			if (error && error.data && error.data.Message) {
				toastr.error('Can\'t edit node of the route', 'Error');
			}
		});
	};


	$scope.updateRouteNode = function($event, node) {
		$scope.closeOptions = true;
	};

	$scope.dotClick = function ($event, node, dot) {
		dot.selected = !dot.selected;
		if ($event && $event.stopPropagation) {
			$event.stopPropagation();
			//$event.preventDefault();
			
			if ($($event.currentTarget).hasClass('node-options-apply')) {
				$scope.closeOptions = true;
			} else {
				$scope.closeOptions = false;
			}
		}
		
		
		if (dot.selected) {
			$scope.addDotToNode(node, dot.Id);
		} else {
			$scope.removeDotFromNode($event, node, dot.Id);
		}
	};

	$scope.addDotToNode = function (node, dotId) {
		var postData = {
			'routeId': $scope.routeId,
			'nodeId': node.Id,
			'dotId': dotId
		};

		routesService.addDotToNode(postData).then(function (response) {
			if (response.data) {
				var result = angular.fromJson(response.data);
				var routeNode = _.find($scope.route.Config.Nodes, function (q) { return q.Id === result.Id; });
				var index = $scope.route.Config.Nodes.indexOf(routeNode);
				if (!$scope.route.Config.Nodes[index].NodeDots)
					$scope.route.Config.Nodes[index].NodeDots = [];
				angular.copy(result.NodeDots, $scope.route.Config.Nodes[index].NodeDots);
				$scope.route.Config.Nodes[index].selected = true;
			} else {
				toastr.error('Can\'t edit node of the route', 'Error');
			}

		}, function (error) {
			if (error && error.data && error.data.Message) {
				toastr.error('Can\'t edit node of the route', 'Error');
			}
		});
	};

	$scope.removeDotFromNode = function ($event, node, dotId) {
		//if ($event && $event.stopPropagation) {
		//	$event.stopPropagation();
		//	//$event.preventDefault();
		var closeIt = false;
		if ($($event.currentTarget).hasClass('node-options-apply') || $($event.currentTarget).hasClass('btn')) {
			$scope.closeOptions = true;
			closeIt = true;

			//	} else {
			//		$scope.closeOptions = false;
			//	}
		}

		var postData = {
			'routeId': $scope.routeId,
			'nodeId': node.Id,
			'dotId': dotId
		};
		
		routesService.removeDotFromNode(postData).then(function (response) {
			if (response.data) {
				var result = angular.fromJson(response.data);
				var routeNode = _.find($scope.route.Config.Nodes, function (q) { return q.Id === result.Id; });
				var index = $scope.route.Config.Nodes.indexOf(routeNode);
				if (!$scope.route.Config.Nodes[index].NodeDots)
					$scope.route.Config.Nodes[index].NodeDots = [];
				angular.copy(result.NodeDots, $scope.route.Config.Nodes[index].NodeDots);
				$scope.route.Config.Nodes[index].selected = !closeIt;
			} else {
				toastr.error('Can\'t edit node of the route', 'Error');
			}

		}, function (error) {
			if (error && error.data && error.data.Message) {
				toastr.error('Can\'t edit node of the route', 'Error');
			}
		});
	};

	$scope.deleteNode = function ($event, nodeId, position) {
		$event.stopPropagation();
		$event.preventDefault();
		var postData = {
			'id': $scope.routeId,
			'nodeId': nodeId,
			'position': position
		};

		routesService.deleteNode(postData).then(function (response) {
			$scope.updateRoute();
		}, function (error) {
			if (error && error.data && error.data.Message) {
				toastr.error('Can\'t delete node from the route', 'Error');
			}
		});
	};

	$scope.selectChoice = function (item) {
		_.each($scope.currentPage.questions, function (q) {
			q.selected = false;
		});
		var question = _.find($scope.currentPage.questions, function (q) { return q.id === item.id });
		if (question) {
			question.selected = true;
		}
	};

	$scope.optionsUrl = function () {
		return "/wwwroot/src/apps/routes-editor/templates/NodeOptions.html";
	};

	$scope.optionsResponseUrl = function () {
		return "/wwwroot/src/apps/routes-editor/templates/NodeDotOptions.html";
	};

	$scope.getNodeIcon = function (node) {
	 switch (node.RoutineGroupId) {
		case 1:
			g.Icon = 'fa-windows';
			break;
		case 3:
			g.Icon = 'fa-sign-in';
			break;
		case 4:
			g.Icon = 'fa-sign-out';
			break;
		case 5:
			g.Icon = 'fa-flag-checkered';
			break;
		case 6:
			g.Icon = 'fa-flask';
			break;
		case 7:
			g.Icon = 'fa-weight';
			break;
		case 8:
			g.Icon = 'fa-chain-broken';
			break;
		case 9:
			g.Icon = 'fa-cubes';
			break;
		case 10:
			g.Icon = 'fa-wheelchair';
			break;
		default:
			g.Icon = 'fa-question';

		}
		return '';
	};

	$scope.getNodeName = function (node) {
		return node.GroupName;
	};

	$scope.getQuestionIcon = function (question) {
		switch (question.family) {
			case 'single_choice':
				if (question.subtype === 'vertical') {
					return 'radio_button_checked';
				}
				if (question.subtype === 'menu') {
					return 'arrow_drop_down_circle';
				}
				break;
			case 'multiple_choice':
				return 'check_circle';
			case 'open_ended':
				if (question.subtype === 'single') {
					return 'short_text';
				}
				if (question.subtype === 'multi') {
					return 'view_headline';
				}
				break;
			case 'datetime':
				return 'date_range';
		}
		return '';
	};

	$scope.editChoice = function ($event, choice) {
		$event.stopPropagation();
		$event.preventDefault();
	};

	$scope.deleteChoice = function ($event, choice, question) {
		$event.stopPropagation();
		$event.preventDefault();
		question.answers.choices = _.without(question.answers.choices, choice);
		$scope.updateQuestion($event, question);
	};

	$scope.deleteRow = function ($event, row, question) {
		$event.stopPropagation();
		$event.preventDefault();
		question.answers.rows = _.without(question.answers.rows, row);
		$scope.updateQuestion($event, question);
	};


	$scope.addChoice = function ($event, question) {
		$event.stopPropagation();
		$event.preventDefault();
		var lastChoice = _.max(question.answers.choices, function (ch) {
			return ch.position;
		});
		question.answers.choices.push({
			position: (lastChoice.position + 1),
			visible: true,
			text: 'New choice'
		});
		$scope.updateQuestion($event, question);
	};

	$scope.addRow = function ($event, question) {
		$event.stopPropagation();
		$event.preventDefault();

		var lastRow = _.max(question.answers.row, function (row) {
			return row.position;
		});
		question.answers.rows.push({
			position: (lastRow.position + 1),
			visible: true,
			text: 'New Text Row'
		});
		$scope.updateQuestion($event, question);
	};

	//$scope.showAddChoice = function (question) {
	//	return (question.family === 'multiple_choice') || (question.family === 'single_choice');
	//};

	//$scope.showAddRow = function (question) {
	//	return (question.family === 'open_ended') && (question.subtype === 'multi');
	//};

	//$scope.isSingleChoice = function(question) {
	//	return (question.family === 'single_choice') && (question.subtype === 'vertical');
	//};

	//$scope.isMultiChoice = function(question) {
	//	return (question.family === 'multiple_choice') && (question.subtype === 'vertical');
	//};

	//$scope.isDropdown = function(question) {
	//	return (question.family === 'single_choice') && (question.subtype === 'menu');
	//};

	//$scope.isSingleTextBox = function (question) {
	//	return (question.family === 'open_ended') && (question.subtype === 'single');
	//};

	//$scope.isMultiTextBox = function (question) {
	//	return (question.family === 'open_ended') && (question.subtype === 'multi');
	//};

	//$scope.isDateTime = function(question) {
	//	return (question.family === 'datetime') &&
	//		(question.subtype === 'both' || question.subtype === 'date_only' || question.subtype === 'time_only');
	//};

	$scope.toggleRequired = function (question) {
		if (!question.isRequired) {
			question.required = null;
		} else {
			question.required = {
				text: 'This question requires an answer.',
				amount: '0',
				type: 'all'
			};
		}
	};

	$scope.isRequired = function (question) {
		if (question.hasOwnProperty('required') && question.required != null) {
			return true;
		}
		return false;
	};

	$scope.updateQuestion = function ($event, question) {
		if ($event && $event.stopPropagation) {
			$event.stopPropagation();
			$event.preventDefault();
			if ($($event.currentTarget).hasClass('question-options-apply')) {
				$scope.closeOptions = true;
			}
		}
		//question.selected = false;

		var postData = {
			'id': $scope.surveyId,
			'pageId': $scope.currentPage.id,
			'question': question
		};

		routesService.updateQuestion(postData).then(function (response) {
			console.log(response.data);
		}, function (error) {
			question.selected = false;
			if (error && error.data && error.data.Message) {
				toastr.error('Can\'t update question properties', 'Error');
			}
		});
	};

};


routesEditorController.$inject = ['$scope', '$uibModal', '$timeout', 'routesService', '$location', '$window', 'toastr', '$interval', '$http'];
routesEditorApp.controller("routesEditorController", routesEditorController);
