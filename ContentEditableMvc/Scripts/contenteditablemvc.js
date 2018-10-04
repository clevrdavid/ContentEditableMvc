$(function () {

    var currentEditingWrapper;
    var dropdownFocussed;
    var timeout;

    $(document).on('focus','.cem-content', function () {
        var cemWrapper = $(this).parent();
        if (currentEditingWrapper !== cemWrapper)
            startEditing(cemWrapper);
    });

    $(document).on('mousedown', '.cem-savechanges', function () {
        var cemWrapper = $(this).closest('.cem-wrapper');

        saveChanges(cemWrapper);
    });

    //  No need for .cem-discardchanges - clicking it blurs the input, so it discards the changes anyway.
    $(document).on('click', '.cem-discardchanges', function () {

        var cemWrapper = $(this).closest('.cem-wrapper');

        stopEditing(cemWrapper);
    });

    function blurTimeout(cemContent) {
        var cemWrapper = cemContent.parent();
        stopEditing(cemWrapper);
    }

    $(document).on('blur', '.cem-content', function () {

        var isDropDown = $(this).attr('data-dropdown');
        if (isDropDown === "true") return;
        var cemContent = $(this);
        window.setTimeout(function () {
            blurTimeout(cemContent);
        }, timeout);
    });

   

    $(document).on('keypress', '.cem-content', function (event) {
        if (event.keyCode === 10 || event.keyCode === 13) {
            var allowMultiline = $(this).attr('data-multiline');
            var isDropDown = $(this).attr('data-dropdown');

            //  If we're not allowing multiline or dropdown mode, save changes instead.
            if (allowMultiline !== "true" && isDropDown !== "true") {
                event.preventDefault();
                var cemWrapper = $(this).closest('.cem-wrapper');
                saveChanges($(cemWrapper));
                $(this).blur();
                return false;
            }
        }
        return true;
    });

    $(document).on("change", '.cem-dropdownbox', function (event) {
        //find closest, change text value.

        var div = $(this).closest('.cem-wrapper').find('.cem-content');

        div.attr('value', $(this).val());

        div.text($(this).find('option:selected').text());

       
      //   $(this).closest('.cem-wrapper').find('.cem-content').html($(this).val());
    });

    function saveChanges(cemWrapper) {


        var cemContent = cemWrapper.find('.cem-content');

        var newVal = cemContent.html();

        cemWrapper.data('original', null);

        if (cemContent.attr('value') !== null) {
            newVal = cemContent.attr('value');
        }

        var data = {
            PropertyName: cemContent.attr('data-property-name'),
            NewValue: newVal,
            RawModelData: cemContent.attr('data-model-data')
        };

        $.ajax({
            type: 'POST',
            url: cemContent.attr('data-edit-url'),
            data: data,
            success: function (response) {
                if (response.success) {
                   // $.growl.notice({ title:"Success", message: "Changes Saved" });
                    endEditing(cemWrapper);
                }
                else {
                    var msgText = "";

                    if (response.message !== null)
                        msgText = response.msg;
                    else
                        msg = "Couldn't Save Changes";

                   // $.growl.error({ message: msgText });
                    stopEditing(cemWrapper);
                }
                
            },
            error: function () {
               // $.growl.error({ message: "Error Saving Change" });
                stopEditing(cemWrapper);
            }
        });
    }

    function startEditing(cemWrapper) {
        cemWrapper.addClass('cem-editing');
        cemWrapper.children('.cem-toolbar').show();
        var cemContent = cemWrapper.find('.cem-content');

        //  Store the current state.
        currentEditingWrapper = cemWrapper;
        cemWrapper.data('original', cemContent.html());
    }

    function stopEditing(cemWrapper) {
        cemWrapper.removeClass('cem-editing');
        cemWrapper.children('.cem-toolbar').hide();

        //  Get the content.
        var cemContent = cemWrapper.find('.cem-content');

        //  If we have an original value, set it.
        if (cemWrapper.data('original') !== null)
            cemContent.html(cemWrapper.data('original'));
    }

    function endEditing(cemWrapper) {

        cemWrapper.removeClass('cem-editing');
        cemWrapper.children('.cem-toolbar').hide();

        //  Get the content.
        //var cemContent = cemWrapper.find('.cem-content');

        ////  If we have an original value, set it.
        //if (cemWrapper.data('original') != null)
        //    cemContent.html(cemWrapper.data('original'));
    }
});