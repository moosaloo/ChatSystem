angular.module('app').controller('mainCtrl',['$scope','chat','toastr','$http', function ($scope, chat, toastr,$http) {


    $scope.messages = [];

    $scope.inRoom = false;


    $scope.joinRoom = function () {
        $scope.inRoom = true;
        chat.server.joinRoom($('#roomNames').val());
    }

    $scope.loadOldMessages = function () {

        $http.get('/Home/GetRoomMessages', { params: { roomName: $('#roomNames').val() } }).success(function (data) {
            $scope.messages = data;
            $scope.$apply();
        })
    }
    $scope.leaveRoom = function () {
        $scope.messages=[];
        $scope.$apply();
        $scope.inRoom = false;
        chat.server.leaveRoom($('#roomNames').val());
    }

    $scope.sendMessage = function () {
        chat.server.sendMessage($('#roomNames').val(), $scope.newMessage);
        displayMessage("شما :  "+$scope.newMessage);
        $scope.newMessage = "";
    };

    chat.client.newMessage = onNewMessage;

    function onNewMessage(message) {
        displayMessage(message);
        $scope.$apply();
        console.log(message);
    };

    function displayMessage(message) {
        $scope.messages.push({ message: message });
    }

    chat.client.newNotification = function onNewNotification(notification) {
        toastr.success(notification);
    }
}]);

