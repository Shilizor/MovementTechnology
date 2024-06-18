const modalContainer = document.querySelector('.modal-container');
const modalButton = document.querySelector('.modal-button');
const modalClose = document.querySelector('.modal-close');
// modalButton.addEventListener('click', () => {
//     modalContainer.classList.add('modal-open');
// });
modalClose.addEventListener('click', () => {
    modalContainer.classList.remove('modal-open');
});
modalContainer.addEventListener('click', (event) => {
    if (event.target === modalContainer) {
        modalContainer.classList.remove('modal-open');
    }
});
AddOrEditDep = (form) =>
{
    try {
        $.ajax({
            type: "POST",
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (result) {
                if(result.isValid == true){
                    $("#depToUpdate1").html(result.url);
                    modalContainer.classList.remove('modal-open');
                    AllDepartaments();
                }
                else {
                    $("#depToUpdate1").html(result.url);
                }
            },
            error: function (err)
            {
                console.log(err)
            }
        })
        
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

RemoveDepData = form => {
    if (confirm('Вы действительно хотите удалить запись?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    alert("Удалено");
                    $("#depToUpdate").html(res.url);
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }

    //prevent default form submit event
    return false;
}

AddOrEditStaff = (form) =>
{
    try {
        $.ajax({
            type: "POST",
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (result) {
                alert(result.isValid);
                if(result.isValid == true){
                    alert("успех");
                    $("#depToUpdate1").html(result.url);
                    modalContainer.classList.remove('modal-open');
                    AllStaffs();
                }
                else {
                    alert("не успех")
                    $("#depToUpdate1").html(result.url);
                }
            },
            error: function (err)
            {
                console.log(err)
            }
        })

        return false;
    } catch (ex) {
        console.log(ex)
    }
}

RemoveStaffData = form => {
    if (confirm('Вы действительно хотите удалить запись?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    alert("Удалено");
                    $("#depToUpdate").html(res.url);
                    AllStaffs();
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }

    //prevent default form submit event
    return false;
}


showInPopupHistory = (url) =>
{
    const modalContainer = document.querySelector('.modal-container');
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $("#depToUpdate1").html(res);
            modalContainer.classList.add('modal-open');
        }
    })
}

showInPopup = (url) =>
{
    const modalContainer = document.querySelector('.modal-container');
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            if(res.isInsert == true){
                $("#depToUpdate1").html(res.url);
                modalContainer.classList.add('modal-open');
                $(".form_submit_btn").val('Добавить');
            }
            else {
                $("#depToUpdate1").html(res.url);
                modalContainer.classList.add('modal-open');
                $(".form_submit_btn").val('Изменить');
            }
            
        }
    })
}