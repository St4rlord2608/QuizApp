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
