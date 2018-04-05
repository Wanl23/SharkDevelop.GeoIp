function locateIp() {
    var ip = document.getElementById("txtIpAddress").value;
    var locatorUrl = "http:////localhost:50529//api//LocateCountry?ipAddress=" + ip;
    var country = document.getElementById("lblCountry");

    $.get(
        locatorUrl,
        function (data) {
            country.innerText = data;
        })
        .fail(function () {
            country.innerText = "Страна не найдена.";
        }
    );

    return false;
}