$(document).ready(function (e) {
    $(".input-autocomplete").autocomplete({
        source: function (request, response) {
            console.log(request);
            $.ajax({
                url: "/Rides/GetCities",
                type: "GET",
                dataType: "json",
                data: request,
                success: function (data) {
                    var escapedTerm = request.term.replace(/([\^\$\(\)\[\]\{\}\*\.\+\?\|\\])/gi, "\\$1");
                    var regex = new RegExp("(?![^&;]+;)(?!<[^<>]*)(" + escapedTerm + ")(?![^<>]*>)(?![^&;]+;)", "gi");
                    var result = $.map(data.results, function (value) {
                        value = value.replace(regex, "<span class='highlight'>$1</span>");
                        return value;
                    });
                    response(result);
                },
                error: function (request, status, error) {
                    console.log(request.responseText);
                }
            });
        },
        minLength: 3
        //create: function (event, ui) {
        //    $(this).data('ui-autocomplete')._renderItem = function (ul, item) {
        //        return $("<li></li>")
        //            .data("item.autocomplete", item)
        //            .append('<a>' + item.label + '</a>')
        //            .appendTo(ul);
        //    };
        //}
    }).data('ui-autocomplete')._renderItem = function (ul, item) {
        var listItem = $('<li></li>').data('ui-autocomplete-item', item);
        //the re and t variables make the user's typed characters show in bold blue in all dropdown items.
        var re = new RegExp('^' + this.term, 'i'); //i makes the regex case insensitive

        //change initial typed characters of all item.label   replace is plain JS
        var t = item.label.replace(re, '<span class=required-drop>' + this.term + '</span>');

        listItem.html('<a>' + t + '</a>');
        return listItem.appendTo(ul);
    }; //end of renderItem function

});