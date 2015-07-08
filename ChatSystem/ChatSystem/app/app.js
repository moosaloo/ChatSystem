(function () {
    angular.module('app', []);

    $(function () {
        $.connection.hub.logging = true;
        $.connection.hub.start();
    });

    angular.module('app')
        .value('chat', $.connection.chat)
        .value('toastr', toastr);
})();