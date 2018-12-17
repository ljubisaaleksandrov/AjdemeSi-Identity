var driverNewRideForm = '#DriverNewRide';
var placeFrom = '.driver-ride-field-place-from';
var placeTo = '.driver-ride-field-place-to';
var dateFrom = '.driver-ride-field-date-from';
var timeFrom = '.driver-ride-field-start-time';
var timeDelay = '.driver-ride-field-dalayed-start';
var timeTravel = '.driver-ride-field-travel-time';
var timeBreak = '.driver-ride-field-break-time';
var rsWrapper = '.driver-ride-field-arrival-wrapper';
var rsPrice = '.driver-ride-price';
var rsPriceOption = '.driver-ride-price-option';
var rsPassengersNo = '.driver-ride-field-passengers-number';
var rSDestination = "#RideDestination";
var rSDate = "#RideDate";
var rSTravelTime = "#RideTravelTime";
var rSStartTime = "#RideStartTime";
var rSArrivalTime = "#RideArivalTime";
var rSArrivalTimeXS = "RideArivalTimeXS";
var rSStartTimeMeridian = "#RideStartTimeMeridian";
var rSArrivalTimeMeridian = "#RideArrivalTimeMeridian";
var rsPassengersNoFinal = "#RidePassengersNo";
var rsSummaryPrice = "#RidePrice";
var btnCreateRide = "#CreateRide";

var placeFromReturn = '.driver-ride-return-field-place-from';
var placeToReturn = '.driver-ride-return-field-place-to';
var dateFromReturn = '.driver-ride-return-field-date-from';
var timeFromReturn = '.driver-ride-return-field-start-time';
var timeDelayReturn = '.driver-ride-return-field-dalayed-start';
var timeTravelReturn = '.driver-ride-return-field-travel-time';
var timeBreakReturn = '.driver-ride-return-field-break-time';
var returnRideEnabled = '.driver-ride-return-return-enabled-checkbox';
var rsrWrapper = '.driver-ride-return-field-arrival-wrapper';
var rsrPrice = '.driver-ride-return-price';
var rsrPriceOption = '.driver-ride-return-price-option';
var rsrPassengersNo = '.driver-ride-return-field-passengers-number';
var rrSDestination = "#ReturnRideDestination";
var rrSDate = "#ReturnRideDate";
var rrSTravelTime = "#ReturnRideTravelTime";
var rrSStartTime = "#ReturnRideStartTime";
var rrSArrivalTime = "#ReturnRideArivalTime";
var rrSArrivalTimeXS = "ReturnRideArivalTimeXS";
var rrSStartTimeMeridian = "#ReturnRideStartTimeMeridian";
var rrSArrivalTimeMeridian = "#ReturnRideArrivalTimeMeridian";
var rrsPassengersNoFinal = "#ReturnRidePassengersNo";
var rrsSummaryPrice = "#ReturnRidePrice";
var btnCreateRideReturn = "#CreateRideReturn";

var isReturnRideEnabled = $(returnRideEnabled).prop('checked');


var lineDelimiter = ' - ';
var blankDelimiter = ' ';
var timeDelimiter = ':';
var bracketsPatter = /\(([^)]+)\)/;

var timeEmpty = "00:00";


var addTime = function (baseTime, timeToAdd) {
    var baseTimeH = baseTime != '' ? parseInt(baseTime.split(timeDelimiter)[0]) : 0;
    var baseTimeM = baseTime != '' ? parseInt(baseTime.split(timeDelimiter)[1]) : 0;
    var timeToAddH = timeToAdd != '' ? parseInt(timeToAdd.split(timeDelimiter)[0]) : 0;
    var timeToAddM = timeToAdd != '' ? parseInt(timeToAdd.split(timeDelimiter)[1]) : 0;

    var minutesSum = baseTimeM + timeToAddM;
    var hoursSum = baseTimeH + timeToAddH;
    var daysSum = 0;

    if (minutesSum >= 60) {
        minutesSum -= 60;
        hoursSum++;
    }
    if (hoursSum >= 24) {
        hoursSum -= 24;
        daysSum = 1;
    }

    var result = {
        days: daysSum,
        hours: hoursSum,
        minutes: minutesSum
    };

    return result;
};

var formatLeadingZero = function (number) {
    return number >= 10 ? number : "0" + number;
};

var toString = function (day, minute) {
    return formatLeadingZero(day) + timeDelimiter + formatLeadingZero(minute);
};

var toMeridianTime = function (time) {
    var timeHI = parseInt(time.split(timeDelimiter)[0]);
    var timeM = time.split(timeDelimiter)[1];

    if (timeHI >= 12) {
        return formatLeadingZero(timeHI - 12) + timeDelimiter + timeM + blankDelimiter + "PM";
    }
    else {
        return formatLeadingZero(timeHI) + timeDelimiter + timeM + blankDelimiter + "AM";
    }
};

var checkCreateRideFormFields = function (formElement) {
    var placeFromValue = $(placeFrom).val();
    var placeToValue = $(placeTo).val();
    var dateFromValue = $(dateFrom).val();
    var timeFromValue = $(timeFrom).val();
    var timeDelayValue = $(timeDelay).val();
    var timeTravelValue = $(timeTravel).val();
    var timeBreakValue = $(timeBreak).val();
    var price = $(rsPrice).val();
    var isTotalPrice = $(rsPriceOption).val() === "TP";
    var passengersNo = $(rsPassengersNo).val();

    var isPlaceFromValid = bracketsPatter.exec(placeFromValue) !== null;
    var isPlaceToValid = bracketsPatter.exec(placeToValue) !== null;

    if (isPlaceFromValid && isPlaceToValid && dateFromValue !== '' && timeFromValue !== '' && passengersNo !== '') {
        timeTravelValue = timeTravelValue !== '' ? timeTravelValue : 0;
        price = price !== '' ? price : 0;

        $(rSDestination).children('.driver-ride-summary-from').html(placeFromValue);
        $(rSDestination).children('.driver-ride-summary-to').html(placeToValue);
        
        $(rSDate).html(dateFromValue);

        if (timeDelayValue === '') {
            $(rSStartTime).html(timeFromValue);
            $(rSStartTimeMeridian).html(toMeridianTime(timeFromValue));
        }
        else {
            var delayedTimeObject = addTime(timeFromValue, timeDelayValue);
            var delayTime = toString(delayedTimeObject.hours, delayedTimeObject.minutes);
            $(rSStartTime).html(timeFromValue + lineDelimiter + delayTime);
            $(rSStartTimeMeridian).html(toMeridianTime(timeFromValue) + lineDelimiter + toMeridianTime(delayTime));
        }

        var totalTravelTime = addTime(timeTravelValue, timeEmpty);
        var timeToValue = addTime(timeFromValue, toString(totalTravelTime.hours, totalTravelTime.minutes));
        var timeToValueFormated = toString(timeToValue.hours, timeToValue.minutes);
        if (timeDelayValue === '') {
            $(rSArrivalTime).html(timeToValueFormated);
            $(rSArrivalTimeXS).html(timeToValueFormated);
            $(rSArrivalTimeMeridian).html(toMeridianTime(timeToValueFormated));
        }
        else {
            var delayedTimeObject = addTime(timeToValueFormated, timeDelayValue);
            var delayTime = toString(delayedTimeObject.hours, delayedTimeObject.minutes);
            var fullDelayedTimeObject = addTime(delayTime, timeBreakValue);
            var fullDelayTime = toString(fullDelayedTimeObject.hours, fullDelayedTimeObject.minutes);
            $(rSArrivalTime).html(timeToValueFormated + lineDelimiter + fullDelayTime);
            $(rSArrivalTimeXS).html(timeToValueFormated);
            $(rSArrivalTimeMeridian).html(toMeridianTime(timeToValueFormated) + lineDelimiter + toMeridianTime(fullDelayTime));
        }

        $(rsPassengersNoFinal).text(passengersNo);
        $(rsSummaryPrice).text($(rsPriceOption + " option:selected").text().substring(1) + ": " + price);

        $(rsWrapper).addClass("show");
        $(btnCreateRide).prop("disabled", false);


        // ---------------- return ride enabled ----------------

        if (isReturnRideEnabled) {
            var returnPlaceFromValue = $(placeFromReturn).val();
            var returnPlaceToValue = $(placeToReturn).val();
            var returnDateFromValue = $(dateFromReturn).val();
            var returnTimeFromValue = $(timeFromReturn).val();
            var returnTimeDelayValue = $(timeDelayReturn).val();
            var returnTimeTravelValue = $(timeTravelReturn).val();
            var returnTimeBreakValue = $(timeBreakReturn).val();
            var returnPrice = $(rsrPrice).val();
            var returnIsTotalPrice = $(rsrPriceOption).val() === "TP";
            var returnPassengersNo = $(rsrPassengersNo).val();

            var returnIsPlaceFromValid = bracketsPatter.exec(returnPlaceFromValue) !== null;
            var returnIsPlaceToValid = bracketsPatter.exec(returnPlaceToValue) !== null;

            if (returnIsPlaceFromValid && returnIsPlaceToValid && returnDateFromValue !== '' && returnPassengersNo !== '') {
                returnTimeFromValue = returnTimeFromValue !== '' ? returnTimeFromValue : timeEmpty;
                returnTimeTravelValue = returnTimeTravelValue !== '' ? returnTimeTravelValue : timeEmpty;
                returnPrice = returnPrice !== '' ? returnPrice : timeEmpty;
                returnPassengersNo = returnPassengersNo !== '' ? returnPassengersNo : timeEmpty;

                $(rrSDate).html(returnDateFromValue);

                if (returnTimeDelayValue === '') {
                    $(rrSStartTime).html(returnTimeFromValue);
                    $(rrSStartTimeMeridian).html(toMeridianTime(returnTimeFromValue));
                }
                else {
                    var delayedTimeObject = addTime(returnTimeFromValue, returnTimeDelayValue);
                    var delayTime = toString(delayedTimeObject.hours, delayedTimeObject.minutes);
                    $(rrSStartTime).html(returnTimeFromValue + lineDelimiter + delayTime);
                    $(rrSStartTimeMeridian).html(toMeridianTime(returnTimeFromValue) + lineDelimiter + toMeridianTime(delayTime));
                }

                var totalTravelTime = addTime(returnTimeTravelValue, timeEmpty);
                var timeToValue = addTime(returnTimeFromValue, toString(totalTravelTime.hours, totalTravelTime.minutes));
                var timeToValueFormated = toString(timeToValue.hours, timeToValue.minutes);
                if (returnTimeDelayValue === '') {
                    $(rrSArrivalTime).html(timeToValueFormated);
                    $(rrSArrivalTimeMeridian).html(toMeridianTime(timeToValueFormated));
                }
                else {
                    var delayedTimeObject = addTime(timeToValueFormated, returnTimeDelayValue);
                    var delayTime = toString(delayedTimeObject.hours, delayedTimeObject.minutes);
                    var fullDelayedTimeObject = addTime(delayTime, returnTimeBreakValue);
                    var fullDelayTime = toString(fullDelayedTimeObject.hours, fullDelayedTimeObject.minutes);
                    $(rrSArrivalTime).html(timeToValueFormated + lineDelimiter + fullDelayTime);
                    $(rrSArrivalTimeMeridian).html(toMeridianTime(timeToValueFormated) + lineDelimiter + toMeridianTime(fullDelayTime));
                }

                $(rrsPassengersNoFinal).text(returnPassengersNo);
                $(rrsSummaryPrice).text($(rsrPriceOption + " option:selected").text().substring(1) + ": " + returnPrice);

                $(rsrWrapper).addClass("show");
                $(rsWrapper).addClass("rider-return");
                $(btnCreateRideReturn).prop("disabled", false);
            }
            else {
                $(btnCreateRideReturn).prop("disabled", true);
                $(rsWrapper).removeClass("rider-return");
            }
        }
    }
    else {
        $(btnCreateRide).prop("disabled", true);
        $(btnCreateRideReturn).prop("disabled", true);
    }

};


$(document).ready(function (e) {
    $.each($('.input-autocomplete'), function (index, input) {
        $(input).autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/en/Rides/GetCities",
                    type: "GET",
                    dataType: "json",
                    data: request,
                    success: function (data) {
                        var escapedTerm = request.term.replace(/([\^\$\(\)\[\]\{\}\*\.\+\?\|\\])/gi, "\\$1");
                        var regex = new RegExp("(?![^&;]+;)(?!<[^<>]*)(" + escapedTerm + ")(?![^<>]*>)(?![^&;]+;)", "gi");
                        var result = $.map(data.results, function (value) {
                            var itemLabel = value.replace(regex, "<span class='highlight'>$1</span>");
                            return {
                                label: itemLabel,
                                value: value
                            };
                        });
                        response(result);
                    },
                    error: function (request, status, error) {
                        console.log(request.responseText);
                    }
                });
            },
            select: function (event, ui) {
                event.preventDefault();
                event.stopPropagation();
                //var value = typeof $(ui.item).data('value') !== 'undefined' ? $(ui.item).data('value') : $(ui.item.label).data('value');
                $(this).val(ui.item.value);
                checkCreateRideFormFields($(this).parents(driverNewRideForm)[0]);
            },
            focus: function (event, ui) {
                event.preventDefault();
                event.stopPropagation();
                //var value = typeof $(ui.item).data('value') !== 'undefined' ? $(ui.item).data('value') : $(ui.item.label).data('value');
                $(this).val(ui.item.value);

                if (isReturnRideEnabled) {
                    $(placeFromReturn).val($(placeTo).val());
                    $(placeToReturn).val($(placeFrom).val());
                }
            },
            minLength: 2
        }).focus(function (event) {
            event.preventDefault();
            $(this).autocomplete("search");
        }).data('ui-autocomplete')._renderItem = function (ul, item) {
            var listItem = $('<li></li>').data('ui-autocomplete-item', item);
            var re = new RegExp('^' + this.term, 'i');
            var t = item.label.replace(re, '<span class=required-drop>' + this.term + '</span>');
            listItem.html('<a>' + t + '</a>');
            return listItem.appendTo(ul);
        };

    });

    $(".input-datepicker").datepicker();

    $.each($('.timepicker'), function (index, timePicker) {
        if ($(timePicker).hasClass(timeTravel.substring(1)) || $(timePicker).hasClass(timeTravelReturn.substring(1))) {
            $(timePicker).timepicki({
                show_meridian: false,
                start_time: ["02", "00"],
                step_size_minutes: 5
            });
        }
        else if ($(timePicker).hasClass(timeDelay.substring(1)) || $(timePicker).hasClass(timeDelayReturn.substring(1))) {
            $(timePicker).timepicki({
                show_meridian: false,
                start_time: ["00", "15"],
                step_size_minutes: 5
            });
        }
        else if ($(timePicker).hasClass(timeBreak.substring(1)) || $(timePicker).hasClass(timeBreakReturn.substring(1))) {
            $(timePicker).timepicki({
                show_meridian: false,
                start_time: ["00", "00"],
                step_size_minutes: 5
            });
        }
        else {
            $(timePicker).timepicki({
                show_meridian: false,
                step_size_minutes: 5
            });
        }
    });
});


$(".timepicker").on("change", function () {
    checkCreateRideFormFields($(this).parents(driverNewRideForm)[0]);
});

$(document).on('change', $('.driver-ride-field .input-datepicker'), function (event) {
    checkCreateRideFormFields($(this).parents(driverNewRideForm)[0]);
});

//$(document).on('change', $('.driver-ride-field .timepicker'), function (event) {
//    console.log(event.currentTarget);
//});

$(document).on('change', '.driver-ride-return-enabled-checkbox', function (event) {
    isReturnRideEnabled = $(event.currentTarget).prop('checked');
    $.each($('.driver-ride-return-fields').find('.driver-ride-field'), function (index, element) {
        $(element).toggleClass('disabled').toggleClass('hidden-xs');
        $(element).find('input:not(' + placeFromReturn + '):not(' + placeToReturn + ')').prop('disabled', !isReturnRideEnabled);
        $(element).find('select').prop('disabled', !isReturnRideEnabled);
        $(element).find('textarea').prop('disabled', !isReturnRideEnabled);
    });

    if (isReturnRideEnabled) {
        $(placeFromReturn).val($(placeTo).val());
        $(placeToReturn).val($(placeFrom).val());
    }

    $('.driver-ride-create-ride-btn').toggleClass('disabled');
    $('.driver-ride-return-create-ride-btn').toggleClass('disabled');
});

var serializeObject = function (form) {
    var o = {};
    var a = form.serializeArray();
    $.each(a, function () {
        if (o[this.name.replace("RideCreationViewModel.", "")]) {
            if (!o[this.name.replace("RideCreationViewModel.", "")].push) {
                o[this.name.replace("RideCreationViewModel.", "")] = [o[this.name.replace("RideCreationViewModel.", "")]];
            }
            o[this.name.replace("RideCreationViewModel.", "")].push(this.value || '');
        } else {
            o[this.name.replace("RideCreationViewModel.", "")] = this.value || '';
        }
    });
    return o;
};

$(btnCreateRide).on("click", function () {
    var data = {
        placeFrom: $(placeFrom).val(),
        placeTo: $(placeTo).val(),
        dateFrom: $(dateFrom).val(),
        timeFrom: $(timeFrom).val(),
        timeDelay: $(timeDelay).val(),
        timeTravel: $(timeTravel).val(),
        timeBreak: $(timeBreak).val(),
        passengersNomber: $(rsPassengersNo).val(),
        isTotalPrice: $(rsPriceOption).val() === "TP",
        Price: $(rsPrice).val(),
        __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
    };

    var token = $('input[name="__RequestVerificationToken"]').val();


    var formData = $(this).parents('form').serialize();
    var aaform = $(this).parents('form').serializeArray();
    var adata = serializeObject($(this).parents('form'));

    $.ajax({
        url: "/en/Rides/CreateRide",
        type: "POST",
        dataType: "json",
        data: {
            __RequestVerificationToken: token,
            model: adata
        },
        success: function (data) {
            console.log(data);
        },
        error: function (request, status, error) {
            console.log(request.responseText);
        }
    });
});
