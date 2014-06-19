'use strict';

var testResultsViewerControllers = angular.module('testResultsViewerControllers', ['testResultsViewerServices']);

testResultsViewerControllers.controller('testResultsController', ['$scope', 'testRunsService',
    function ($scope, testRuns) {
        $scope.testRun = testRuns.get();
    }]);