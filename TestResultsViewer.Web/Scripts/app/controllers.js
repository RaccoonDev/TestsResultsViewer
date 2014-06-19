'use strict';

var testResultsViewerControllers = angular.module('testResultsViewerControllers', ['testResultsViewerServices']);

testResultsViewerControllers.controller('testResultsController', ['$scope', 'testRunsService',
    function ($scope, testRuns) {
        $scope.testInstance = { testsResultsVisible : false };
        $scope.testInstance.data = testRuns.get();

        $scope.triggerDetails = function () {
            $scope.testInstance.testsResultsVisible = !$scope.testInstance.testsResultsVisible;
        }

        $scope.getColorClass = function (outcome) {
            return {
                'green': outcome == 'Passed' || outcome == 'Pass',
                'red': outcome == 'Failed',
                'purple': outcome == 'Inconclusive'
            };
        };

        $scope.getRowColorClass = function (outcome) {
            return {
                'success': outcome == 'Passed' || outcome == 'Pass',
                'error': outcome == 'Failed',
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
    }]);