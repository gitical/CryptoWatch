"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/cryptofeed").build();

connection.on("UpdatePrices", function (feed) {
    console.log(feed);

    document.getElementById("BTCNOK").innerText = feed.find(x => x.id === 'BTCNOK').last;
    document.getElementById("LTCNOK").innerText = feed.find(x => x.id === 'LTCNOK').last;
    document.getElementById("ETHNOK").innerText = feed.find(x => x.id === 'ETHNOK').last;
});

connection.start()
    .catch(function (err) {
        return console.error(err.toString());
    });

