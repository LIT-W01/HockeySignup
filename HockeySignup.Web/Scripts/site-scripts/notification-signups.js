$(function() {

    $(".close").on('click', function() {
        $("#error-div").hide();
    });

    $("#signup-form").on('submit', function() {
        if (isFormValid()) {
            return true;
        }

        showError("Invalid entries, please fix your errors....");
        return false;
    });

    function showError(message) {
        $("#error-div").show();
        $("#error-message").text(message);
    }

    function isFormValid() {
        var firstName = $("#firstName").val().trim();
        var lastName = $("#lastName").val().trim();
        var email = $("#email").val().trim();

        if (!firstName || !lastName || !email) {
            return false;
        }

        return isValidEmail(email);
    }

    function isValidEmail(email) {
        var re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
    }
});