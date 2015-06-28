/// <reference path="../../Scripts/angular.js" />
(function () {
    "use strict";
    angular.module("productManagement")
            .controller("ProductListCtrl", ["productResource", ProductListCtrl]);

    function ProductListCtrl(productResource) {
        var vm = this;

        vm.searchCriteria = "GDN";
        vm.loading = false;
        // $filter: "startswith(ProductCode,'GDF')"

        vm.search = function () {
            vm.loading = true;
            productResource.query({
                $filter: "contains(ProductCode, '" + vm.searchCriteria + "') or contains(ProductName, '" + vm.searchCriteria + "')",
                $orderby: "Price asc"
            }, function (data) {
                
                vm.products = data;
                vm.loading = false;
            });

        };

    }
})();