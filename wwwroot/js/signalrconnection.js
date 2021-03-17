"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/cryptofeed").build();

const btc = "BTCNOK";
const ltc = "LTCNOK";
const eth = "ETHNOK";

connection.on("UpdatePrices", function (feed) {
    console.log(feed);

    document.getElementById(btc).innerText = feed.find(x => x.id === btc).last;
    document.getElementById(ltc).innerText = feed.find(x => x.id === ltc).last;
    document.getElementById(eth).innerText = feed.find(x => x.id === eth).last;
});

connection.start()
    .catch(function (err) {
        return console.error(err.toString());
    });

