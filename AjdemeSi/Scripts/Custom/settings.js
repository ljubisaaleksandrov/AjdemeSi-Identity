var btnNewCar = "#AddNewCar";
var formNewCar = "#FormAddNewCar";
var formNewCarWrapper = "#FormAddNewCarWrapper";
var ExistingCarsWrapper = "#ExistingCars";

var vehicleMakeSelect = '.vehicle-make-select';
var vehicleModelSelect = '.vehicle-model-select';



$(document).on('click', btnNewCar, function (e) {
    e.stopPropagation();
    e.preventDefault();

    var currentCarId = $(formNewCar).find("#Id").val();

    $.ajax({
        url: $(formNewCar).attr('action'),
        data: $(formNewCar).serialize(),
        type: 'POST',
        success: function (data) {
            var newCarId = $(data).find("#Id").val();

            if (currentCarId === "0") {
                if (newCarId === "0") {
                    $(formNewCarWrapper).html(data);
                }
                else {
                    $(ExistingCarsWrapper).append(data);
                }
            }
        },
        error: function (request, status, error) {
            console.log(request.responseText);
        }
    });
});

$(document).on('change', vehicleMakeSelect, function (event) {
    event.preventDefault();

    var selectedValue = $(this).val();
    if (selectedValue !== '') {
        var modelsSelect = $(this).parents('form').find(vehicleModelSelect);

        $.ajax({
            url: "/en/Settings/GetVehicleModels",
            data: {
                make: selectedValue
            },
            type: 'GET',
            success: function (data) {
                var defaultOption = $(modelsSelect)[0].options[0];
                $(modelsSelect).children().remove();
                $(modelsSelect).append(defaultOption);

                $.each(data.results, function (index, element) {
                    $(modelsSelect).append('<option value="' + element + '">' + element + '</option>');
                });
            },
            error: function (request, status, error) {
                console.log(request.responseText);
            }
        });
    }
});

