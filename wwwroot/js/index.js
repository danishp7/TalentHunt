$(function () {

    // signup request
    $("#register").on("click", function (e) {
        var getuser = "";
        $.ajax({
            type: "POST",
            url: "/api/auth/register/",
            data: JSON.stringify({
                userName: $("#userName").val(),
                email: $("#email").val(),
                password: $("#password").val()
            }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            headers: {
                "Content-type": "application/json",
                "Accept": "application/json"
            },
            success: function (user) {
                getuser = user;
                alert(getuser.userName);
                console.log(getuser);
            },
            error: function (data) {
                alert("Something went wrong with the request!" + data.status);
            },
            complete: function () {
                $("#h2").text(getuser.userName);
                $("#h3").text(getuser.email);
            }
        });
        
        return false;
        e.stopPropagation();
    });

    // login request
    $("#login").on("click", function (e) {
        var loginuser = "";
        $.ajax({
            type: "POST",
            url: "/api/auth/login/",
            data: JSON.stringify({
                loginUserName: $("#loginUserName").val(),
                loginUserPassword: $("#loginPassword").val()
            }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            headers: {
                "Content-type": "application/json",
                "Accept": "application/json"
            },
            success: function (loggedinuser) {
                loginuser = loggedinuser;
                alert(loginuser.user.userName + " " + loginuser.user.email);
            },
            error: function (data) {
                alert("Something went wrong with the request!" + data.status);
            },
            complete: function () {
                $("#h2").text(loginuser.user.userName);
                $("#h3").text(loginuser.user.email);
            }
        });
        e.stopPropagation();
        return false;
        
    });
})