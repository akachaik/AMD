﻿/// <reference path="../Scripts/angular.js" />

(function () {
    "use strict";

    angular
        .module("common.services", ["ngResource"])
        .constant("appSettings", { serverPath: "http://localhost:59898" });

})();