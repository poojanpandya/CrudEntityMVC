$(function () {
    // datepicker
    $("#datepicker").datepicker({
        maxDate: 0,
        changeMonth: true,
        changeYear: true,
        showAnim: 'slideDown',
        dateFormat: 'yy-mm-dd'
    }).on('change', function () {
        var age = getAge(this);
        $('#age').val(age);
        /* $('#age').val(age);*/
        console.log(age);
    });
    // Get Age from Date
    function getAge(dateVal) {
        var birthday = new Date(dateVal.value),
            today = new Date(),
            ageInMilliseconds = new Date(today - birthday),
            years = ageInMilliseconds / (24 * 60 * 60 * 1000 * 365.25)
        return Math.floor(years);
    }
    // Image Preview 
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#blah').attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]); // convert to base64 string
        }
    }
    // Image change Function
    $("#imgInp").change(function () {
        $('#filediv').show();
        readURL(this);
    });
    // Get StateList from CountryId
    $("#CountryId").change(function () {
        var countryId = $(this).val();
        $("#StateId").empty();
        $.get('/Common/GetStateListbyCountryId',
            { countryId: countryId }, function (data) {
                var v = "";
                v = "<option value='' selected='selected'>-- Select State --</option>";
                $.each(data, function (i, v1) {
                    v += "<option value=" + v1.Value + ">" + v1.Text + "</option>";
                });

                $("#StateId").html(v);
            });
    });
    // Get CityList from StateId
    $("#StateId").change(function () {
        var stateId = $(this).val();
        $("#CityId").empty();
        $.get('/Common/GetCityListbyStateId',
            { stateId: stateId }, function (data) {
                var v = "";
                v = "<option value='' selected='selected'>-- Select City --</option>";
                $.each(data, function (i, v1) {
                    v += "<option value=" + v1.Value + ">" + v1.Text + "</option>";
                });
                $("#CityId").html(v);
            });
    });
    // Get Price from ProductId
    $("#ProductId").change(function () {
        var productId = $(this).val();
        $.get('/Common/GetPricebyProductId',
            { productId: productId }, function (data) {
                $("#ProductPrice").val(data);
    
            });
    });
    // JQuery Temptable Functionality
    $("#btnAdd").click(function (e) {
        e.preventDefault();
        if ($.trim($("#ProductId").val()) != "") {
            if ($.trim($("#Reciever").val()) != "") {
                var Product = $("#ProductId option:selected").text(),
                    ProductId = $("#ProductId").val(),
                    ProductPrice = $("#ProductPrice").val(),
                    RecieverName = $("#Reciever").val(),
                    detailsTableBody = $("#Producttbl tbody");
                var Item = '<tr><td style="display:none">' + ProductId +'</td><td style="text-align: center;">' + Product + '</td><td style="text-align: center;">' + RecieverName + '</td><td class="pricecls" style="width: 259px;text-align: right;">' + ProductPrice + '</td><td style="text-align: center;"><a data-itemId="0"  class="btn btn-danger" onclick="ActRemove(this);"><i class="fa fa-trash" aria-hidden="true"></i></a></td></tr>';
                detailsTableBody.append(Item);
                //Add Total
                addsum();
                //Clear Data
                OptclearItem();
            }
            else {
                alert("Please Enter Receiver");
            }
        }
        else {
            alert("Please Select Product");
        }
    });

    function databind() {
        var fileUpload = $("#imgInp").get(0);
        var files = fileUpload.files;
        var data = new FormData();
        for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i]);
        }
        // Make a Dynamic Model
        var model = {
            UserId: 0,
            Firstname: "",
            Lastname: "",
            Dateofbirth: "",
            Age: "",
            Gender: "",
            CityId: "",
            Address: "",
            IsChecked: "",
            HobbyId: "",
            HobbyName: "",
            Phoneno: 0,
            Products: [],
            Hobbies: []
        };
        model.UserId = $("#UserId").val();
        model.Firstname = $("#Firstname").val();
        model.Lastname = $("#Lastname").val();
        model.Dateofbirth = $("#datepicker").val();
        model.Age = $("#age").val();
        model.Gender = $('input[name="Gender"]:checked').val();
        model.CityId = $("#CityId").val();
        model.Address = $("#Address").val();
        model.Phoneno = $("#Phoneno").val();
        // Bind Selected Checkbox 
        model.Hobbies = jQuery("#tbl2").serializeArray();
        model.Hobbies = model.Hobbies.concat(
            jQuery('#tbl2 input[type=checkbox]:checked').map(
                function () {
                    return { "HobbyId": this.value, "IsChecked": true }
                }).get()
        );
        // Bind Selected Products
        $.each($("#Producttbl tbody tr"), function () {
            model.Products.push({
                ProductId: $(this).find('td:eq(0)').html(),
                Recievername: $(this).find('td:eq(2)').html()
            });
        });

        data.append("model", JSON.stringify(model));
        return data;
    }

    // Submit Form To Normal Entity
    $("#cmdSubmit").click(function (e) {
        e.preventDefault();
        var data = databind();
        $.ajax({
            type: "POST",
            url: "/CRUD/AddUpdateUser",
            contentType: "application/json",
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                alert("Added Successfully!");
                window.location.href = "/CRUD/Index";
            }
        });
    });
    // Submit Form To Entity SP
    $("#btnsubmit").click(function (e) {
        e.preventDefault();
        var data = databind();
        $.ajax({
            type: "POST",
            url: "/CRUD/AddUpdateUserSP",
            contentType: "application/json",
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                alert("Added Successfully!");
                window.location.href = "/CRUD/Index";
            }
        });
    });   
  
    function OptclearItem() {
        $("#ProductId").val('');
        $("#ProductPrice").val('');
        $("#Reciever").val('');
    }   
});
// Get Total Function
function addsum() {
    var amount_sum = 0;
    $('.pricecls').each(function () {
        amount_sum += Number($(this).html());
    });
    $('.grdtot').val(amount_sum);
}
// Remove Cells Function
function ActRemove(button) {
    //Determine the reference of the Row using the Button.
    var row = $(button).closest("TR");
    if (confirm("Do you want to delete?")) {
        //Get the reference of the Table.
        var table = $("#Producttbl")[0];
        //Delete the Table row using it's Index.
        table.deleteRow(row[0].rowIndex);
        addsum();
    }
};