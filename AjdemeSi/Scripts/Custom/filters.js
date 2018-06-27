var driverNewRideForm = '#DriverNewRide';
var placeFrom = '.driver-ride-field-place-from';
var placeTo = '.driver-ride-field-place-to';
var dateFrom = '.driver-ride-field-date-from';
var timeFrom = '.driver-ride-field-start-time';
var timeDelay = '.driver-ride-field-dalayed-start';
var timeTravel = '.driver-ride-field-travel-time';
var timeBreak = '.driver-ride-field-break-time';
var rsWrapper = '.driver-ride-field-arrival-wrapper';
var rSDestination = "#RideDestination";
var rSDate = "#RideDate";
var rSTravelTime = "#RideTravelTime";
var rSStartTime = "#RideStartTime";
var rSArrivalTime = "#RideArivalTime";
var rSStartTimeMeridian = "#RideStartTimeMeridian";
var rSArrivalTimeMeridian = "#RideArrivalTimeMeridian";

var lineDelimiter = ' - ';
var blankDelimiter = ' ';
var timeDelimiter = ':';


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
}

var formatLeadingZero = function(number){
    return number >= 10 ? number : "0" + number;
}

var toString = function (day, minute) {
    return formatLeadingZero(day) + timeDelimiter + formatLeadingZero(minute);
}

var toMeridianTime = function (time) {
    var timeHI = parseInt(time.split(timeDelimiter)[0]);
    var timeM = time.split(timeDelimiter)[1];

    if (timeHI >= 12) {
        return formatLeadingZero(timeHI - 12) + timeDelimiter + timeM + blankDelimiter + "PM";
    }
    else {
        return formatLeadingZero(timeHI) + timeDelimiter + timeM + blankDelimiter + "AM";
    }
}

var checkCreateRideFormFields = function (formElement) {
    var placeFromValue = $(placeFrom).val();
    var placeToValue = $(placeTo).val();
    var dateFromValue = $(dateFrom).val();
    var timeFromValue = $(timeFrom).val();
    var timeDelayValue = $(timeDelay).val();
    var timeTravelValue = $(timeTravel).val();
    var timeBreakValue = $(timeBreak).val();

    console.log(timeFromValue + " " + timeDelayValue);
    console.log(addTime(timeFromValue, timeDelayValue));

    if (placeFromValue != '' && placeToValue != '' && dateFromValue != '' && timeFromValue != '' && timeTravelValue != '') {
        $(rSDestination).html(placeFromValue + lineDelimiter + placeToValue);
        $(rSDate).html(dateFromValue + lineDelimiter + "????");

        if (timeDelayValue == '') {
            $(rSStartTime).html(timeFromValue);
            $(rSStartTimeMeridian).html(toMeridianTime(timeFromValue));
        }
        else {
            var delayedTimeObject = addTime(timeFromValue, timeDelayValue);
            var delayTime = toString(delayedTimeObject.hours, delayedTimeObject.minutes);
            $(rSStartTime).html(timeFromValue + lineDelimiter + delayTime);
            $(rSStartTimeMeridian).html(toMeridianTime(timeFromValue) + lineDelimiter + toMeridianTime(delayTime));
        }

        var totalTravelTime = addTime(timeTravelValue, timeBreakValue);
        var timeToValue = addTime(timeFromValue, toString(totalTravelTime.hours, totalTravelTime.minutes));
        var timeToValueFormated = toString(timeToValue.hours, timeToValue.minutes);
        if (timeDelayValue == '') {
            $(rSArrivalTime).html(timeToValueFormated);
            $(rSArrivalTimeMeridian).html(toMeridianTime(timeToValueFormated));
        }
        else {
            var delayedTimeObject = addTime(timeToValueFormated, timeDelayValue);
            var delayTime = toString(delayedTimeObject.hours, delayedTimeObject.minutes);
            $(rSArrivalTime).html(timeToValueFormated + lineDelimiter + delayTime);
            $(rSArrivalTimeMeridian).html(toMeridianTime(timeToValueFormated) + lineDelimiter + toMeridianTime(delayTime));
        }

        $(rsWrapper).show();
    }
    else {
        $(rsWrapper).hide();
    }
}


$(document).ready(function (e) {
    $.each($('.input-autocomplete'), function (index, input) {
        $(input).autocomplete({
            source: function (request, response) {
                //request['__RequestVerificationToken'] = $('input[name="__RequestVerificationToken"]', $(driver)).val();
                $.ajax({
                    url: "/Rides/GetCities",
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

        //$(input).on("autocompletechange", function (event, ui) { console.log(ui) });
    });

    $(".input-datepicker").datepicker();

    $.each($('.timepicker'), function (index, timePicker) {
        $(timePicker).timepicki({ show_meridian: false });
    });
});

$(".input-autocomplete").on("change paste keyup", function () {
    alert($(this).val());
});

$(".timepicker").on("change", function () {
    checkCreateRideFormFields($(this).parents(driverNewRideForm)[0]);
});



//$(document).on('change', $('.driver-ride-field .input-datepicker'), function (event) {
//    console.log(event.currentTarget);
//});

//$(document).on('change', $('.driver-ride-field .timepicker'), function (event) {
//    console.log(event.currentTarget);
//});



