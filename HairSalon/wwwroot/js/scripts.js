


$(function(){
    $('.icon-edit').click(function(){
        $(this).nextAll('.edit-form').first().slideToggle();
    });
    $('.icon-edit-specialty').click(function(){
        $(this).parent().hide();
        $(this).parent().nextAll('.edit-specialty-form').first().show();
    })

});