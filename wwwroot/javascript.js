function createCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + value + expires + "; path=/";
}

function getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return null;
}

function AddToLocalStorage(key, value)
{
    localStorage.setItem(key, value);
}

function GetFromLocalStorage(key)
{
    if (localStorage.getItem(key)) {
        return localStorage.getItem(key);
    }
    return null;
}

function UserLeave(userID) {
    var userDataElement = document.getElementById("user-data-" + userID);
    userDataElement.style.opacity = "0.2";
}

function UserRejoin(userID) {
    var userDataElement = document.getElementById("user-data-" + userID);
    userDataElement.style.opacity = "1.0";
}

function InitTooltips() {
        const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
        const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))
}

function AddOnClickBuzzerEventListener() {
    var buzzerElement = document.getElementById("buzzer");
    buzzerElement.addEventListener("click", function (event) {
        PlayBuzzSound();
    })
}

function PlayBuzzSound() {
    var sound = new Audio('/Media/Audio/buzzer.mp3');
    sound.volume = 0.2; 
    sound.play();
}

function PlayCorrectSound() {
    var sound = new Audio('/Media/Audio/correct.mp3');
    sound.volume = 0.2;
    sound.play();
}

function PlayWrongSound() {
    var sound = new Audio('/Media/Audio/wrong.mp3');
    sound.volume = 0.2;
    sound.play();
}

function HandleNearestPlayerVisual(userId, isNearest) {
    var playerCardElement = document.getElementById('user-data-' + userId);
    if (isNearest) {
        playerCardElement.style.border = "3px solid green";
    } else {
        playerCardElement.style.border = "1px solid rgba(0,0,0,.125)"
    }
}
