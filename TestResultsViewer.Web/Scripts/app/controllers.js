'use strict';

var testResultsViewerControllers = angular.module('testResultsViewerControllers', ['testResultsViewerServices']);

testResultsViewerControllers.controller('testResultsController', ['$scope', 'testRunsService',
    function ($scope, testRuns) {
        $scope.testInstances = testRuns.get();

        $scope.getColorClass = function (outcome) {
            return {
                'green': outcome == 'Passed' || outcome == 'Pass' || outcome == 'Completed',
                'red': outcome == 'Failed',
                'purple': outcome == 'Inconclusive'
            };
        };

        $scope.getRowColorClass = function (outcome) {
            return {
                'success': outcome == 'Passed' || outcome == 'Pass',
                'danger': outcome == 'Failed',
                'warning': outcome == 'Inconclusive'
            };
        };

        $scope.trimString = function (str) {
            if (typeof (str) == 'undefined') return '';
            var limit = 90;
            return str.length > limit
                ? str.substring(0, limit) + '...'
                : str;
        };

        $scope.filterDetails = function (testInstance, filterValue) {
            testInstance.testsResultsVisible = true;
            testInstance.detailsFilter = { Outcome: filterValue };
        };

        $scope.predicate = '-Times.Start';
    }]);