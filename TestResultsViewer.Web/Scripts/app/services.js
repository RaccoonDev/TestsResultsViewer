'use strict';

var testResultsViewerServices = angular.module('testResultsViewerServices', ['ngResource']);

testResultsViewerServices.factory('testRunsService', ['$resource',
    function ($resource) {
        return $resource('/ResultsViewer/api/testResultFiles');
    }]);