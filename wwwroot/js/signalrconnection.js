"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/cryptofeed").build();

connection.on("UpdatePrices", function (msg) {
    console.log(msg);
});

connection.start()
    .catch(function (err) {
        return console.error(err.toString());
    });

