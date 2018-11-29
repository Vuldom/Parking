interface ICoord {
    latitude: number;
    longitude: number;
    nameparking: string;
}


$(document).ready(function showMap() {
    let d = document.getElementById('map');
    let map;
    map = new google.maps.Map(d, { center: { lat: 53.9108, lng: 27.5503 }, zoom: 8 });
});


function ShowCoordinates(parkingCoordinates: ICoord, userCoordinates: ICoord): void {

    let markerLatLng = new google.maps.LatLng(userCoordinates.latitude, userCoordinates.longitude);

    let mapOptions = {
        zoom: 11,
        center: markerLatLng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    let map = new google.maps.Map(document.getElementById('map'), mapOptions);

    let markerLabel = 'U';
    let marker = new google.maps.Marker({
        map: map,
        animation: google.maps.Animation.DROP,
        position: markerLatLng,
        label: {
            text: markerLabel,
            color: "#ffffff",
            fontSize: "16px",
            fontWeight: "bold"
        }
    });

    let markerParking = new google.maps.LatLng(parkingCoordinates.latitude, parkingCoordinates.longitude);

    let markerLabelP = 'P';
    let markerP = new google.maps.Marker({
        map: map,
        animation: google.maps.Animation.DROP,
        position: markerParking,
        label: {
            text: markerLabelP,
            color: "#ffffff",
            fontSize: "16px",
            fontWeight: "bold"
        }
    });

    $("#SelectedParking").val(parkingCoordinates.nameparking).formSelect();

}

function success(position) {
    let latitude: number = position.coords.latitude;
    let longitude: number = position.coords.longitude;
    let userCoord = { latitude: position.coords.latitude, longitude: position.coords.longitude, nameparking: null };

    jQuery.get(`Home/FindNearestParking?lat=${userCoord.latitude}&lon=${userCoord.longitude}`, (coord) => { ShowCoordinates(coord, userCoord); })


};


function error() {

};

function findLocation(): void {
    navigator.geolocation.getCurrentPosition(success, error);

    if (!navigator.geolocation) {
        document.getElementById("map").innerHTML = "<p>Geolocation is not supported by your browser</p>";
        return;
    }

}

function showParkingLabel(parkingCoordinates: ICoord): void {

    let markerLatLng = new google.maps.LatLng(parkingCoordinates.latitude, parkingCoordinates.longitude);

    let mapOptions = {
        zoom: 11,
        center: markerLatLng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    let map = new google.maps.Map(document.getElementById('map'), mapOptions);

    let markerLabel = 'P';
    let marker = new google.maps.Marker({
        map: map,
        animation: google.maps.Animation.DROP,
        position: markerLatLng,
        label: {
            text: markerLabel,
            color: "#ffffff",
            fontSize: "16px",
            fontWeight: "bold"
        }
    });
}

function setParkingLabel(): void {

    let selectedParkingName: string = $("#SelectedParking option:selected").text();
    jQuery.get(`Home/FindParkingCoordinates?name=${selectedParkingName}`, (coord) => { showParkingLabel(coord); })

}


