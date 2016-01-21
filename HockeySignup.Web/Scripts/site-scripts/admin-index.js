$(function() {
    $(".max-people").on('change', setButtonState);
    $(".datepicker").on('change', setButtonState);

    function setButtonState() {
        $(".submit-button").prop("disabled", !isFormValid());
    }

    function isFormValid() {
        var maxPeople = parseInt($(".max-people option:selected").val());
        var date = $(".datepicker").val();

        return maxPeople && date;
    }
});