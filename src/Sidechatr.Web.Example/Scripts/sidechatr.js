var sidechatr = function (config) {
    config = config || {};

    var FriendsList = function () {
        var self = this;
        var $el = $('#sidechatr-friends');

        self.update = function (user) {
            $el.prepend('<li><span>' + user.name + '</span>' + user.status + '</li>');
        };

    };

    var $discussion = $('#sidechatr-discussion');

    var addMessage = function (prefix, message) {
        // Html encode display name and message. 
        var encodedMsg = $('<div />').text(message).html();
        // Add the message to the page. 
        $discussion.prepend(
            '<li><span>' + prefix + '</span>'
            + encodedMsg + '</li>');
    };

    var ChatClient = function (config) {
        config = config || {};

        var self = this;
        var friendsList = config.friendsList || { update: function (user) { console.log(user.name + ' updated'); } };
        // Declare a proxy to reference the hub. 
        var chat = $.connection.chatHub;

        // Create a function that the hub can call to broadcast messages.
        chat.client.send = function (user, message) {
            addMessage(user, message);
        };

        chat.client.joined = function (connectionId, timestamp) {
            addMessage(timestamp, connectionId + ' joined');
        };

        chat.client.rejoined = function (connectionId, timestamp) {
            addMessage(timestamp, connectionId + ' rejoined');
        };

        chat.client.leave = function (connectionId, timestamp) {
            addMessage(timestamp, connectionId + ' left');
        };

        chat.client.friendUpdates = function (users) {
            if (!users || !users.length) return;

            var timestamp = new Date();

            var user;
            for (var i = 0; i < users.length; i++) {
                user = users[i];
                //addMessage(timestamp, update.User.name);
                friendsList.update(user);
            }
        };


        // Set initial focus to message input box.  
        $('#message').focus();
        // Start the connection.
        var connect = function () {
            $.connection.hub.qs = 'token=' + config.token;
            $.connection.hub.start().done(function () {
                $('#sendmessage').click(function () {
                    // Call the Send method on the hub. 
                    chat.server.send(
                        config.token, //derp?
                        'recipient',
                        $('#message').val()
                    );
                    // Clear text box and reset focus for next comment. 
                    $('#message').val('').focus();
                });
            });
        }

        var disconnect = function () {
            $('#sendmessage').unbind('click');
            $.connection.hub.stop();
        };

        $('#connect').click(function () {
            connect();
        });

        $('#disconnect').click(function () {
            disconnect();
        });

    };


    var client = new ChatClient({
        token: config.token,
        friendsList: new FriendsList()
    });

    return client;
};