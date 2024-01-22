$(function () {

    var currentEditingWrapper;
    var dropdownFocussed;
    var timeout;


    $(document).on('paste', '.cem-content', function (e) {

        e.preventDefault();

        var text = '';

        if (e.clipboardData || e.originalEvent.clipboardData) {
            text = (e.originalEvent || e).clipboardData.getData('text/plain');
        } else if (window.clipboardData) {
            text = window.clipboardData.getData('Text');
        }

        console.log("Pasted Text: " + text);

        if (document.queryCommandSupported('insertText')) {
            document.execCommand('insertText', false, text);
        } else {
            document.execCommand('paste', false, text);
        }
    });

    $(document).on('focus', '.cem-content', function () {
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

    // function blurTimeout(cemContent) {
    //    
    //     var cemWrapper = cemContent.parent();
    //    
    //     debugger;
    //    
    //     stopEditing(cemWrapper);
    // }
    //
    // $(document).on('blur', '.cem-content', function () {
    //
    //     var isDropDown = $(this).attr('data-dropdown');
    //    
    //     if (isDropDown === "true") return;
    //    
    //     var cemContent = $(this);
    //    
    //     window.setTimeout(function () {
    //         blurTimeout(cemContent);
    //     }, timeout);
    //    
    // });



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

        var oldVal = cemWrapper.data('original');

        console.log("Old Value: " + oldVal);

        if (cemContent.attr('value')) {
            newVal = cemContent.attr('value');
        }

        console.log("New Value: " + newVal);

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

                    $.growl.notice({ title: "Success", message: "Changes Saved" });
                    endEditing(cemWrapper);

                }
                else {

                    var msgText = "";

                    if (response.message !== null)
                        msgText = response.message;
                    else
                        msgText = "Couldn't Save Changes";

                    $.growl.error({ message: msgText });
                    stopEditing(cemWrapper);
                }

            },
            error: function () {

                $.growl.error({ message: "Error Saving Change" });
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

        var originalVal = cemWrapper.data('original');

        debugger;

        //  If we have an original value, set it.
        if (originalVal !== null)
            cemContent.html(originalVal);
    }

    function endEditing(cemWrapper) {

        // debugger;

        cemWrapper.removeClass('cem-editing');
        cemWrapper.children('.cem-toolbar').hide();

        //  Get the content.
        // var cemContent = cemWrapper.find('.cem-content');
        //
        // cemContent.html(cemWrapper.data('newValue'));

        ////  If we have an original value, set it.
        //if (cemWrapper.data('original') != null)
        //    cemContent.html(cemWrapper.data('original'));
    }


    //To address issue where pasted text is not accepted.


});