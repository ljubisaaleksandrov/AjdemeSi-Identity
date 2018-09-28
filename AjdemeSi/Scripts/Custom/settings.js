var btnNewCar = "#AddNewCar";
var formNewCar = "#FormAddNewCar";
var formNewCarWrapper = "#FormAddNewCarWrapper";
var ExistingCarsWrapper = "#ExistingCars";

var fnNewCar = function(e){
    e.stopPropagation();
    e.preventDefault();

    var currentCarId = $(formNewCar).find("#Id").val();

    $.ajax({
        url: $(formNewCar).attr('action'),
        data: $(formNewCar).serialize(),
        type: 'POST',
        success: function (data) {
            var newCarId = $(data).find("#Id").val();

            if (currentCarId == "0") {
                if (newCarId == "0") {
                    $(formNewCarWrapper).html(data);
                }
                else {
                    $(ExistingCarsWrapper).append(data)
                }
            }
        },
        error: function (request, status, error) {
            console.log(request.responseText);
        }
    });
}


$(document).on('click', btnNewCar, fnNewCar);