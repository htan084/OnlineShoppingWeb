function IsFirstNameEmpty() {
    if (document.getElementById('firstName').value == "") {
        return 'First Name should not be empty';
    }
    else { return ""; }
}

function IsFirstNameInValid() {
    if (document.getElementById('firstName').value.indexOf("@") != -1) {
        return 'First Name should not contain @';
    }else if (document.getElementById('firstName').value.length > 35){
        return 'First name cannot be more than 35 characters';
    }
    else { return ""; }
}

function IsAgeValid() {
    if (document.getElementById('age').value == "") {
        return 'age should not be empty';
    } else if (isNaN(document.getElementById('age').value) || (document.getElementById('age').value)<0) {
        return 'age must be a valid number';
    }
    else { return ""; }
}

function IsLastNameInValid() {
    if (document.getElementById('lastName').value.indexOf("@") != -1) {
        return 'Last Name should not contain @';
    } else if (document.getElementById('lastName').value.length >= 10) {
        return 'Last Name should not contain more than 10 character';
    }
    else { return ""; }
}
function IsLastNameEmpty() {
    if (document.getElementById('lastName').value == "") {
        return 'Last Name should not be empty';
    }
    else { return ""; }
}

function IsEmailEmpty() {
    if (document.getElementById('email').value == "") {
        return 'Email should not be empty';
    } else { return ""; }
}

function IsAddressEmpty() {
    if (document.getElementById('address').value == "") {
        return 'Address should not be empty';
    } else { return ""; }
}

function IsMobileEmpty() {
    if (document.getElementById('mobile').value == "") {
        return 'Mobile number should not be empty';
    } else { return ""; }
} function IsUserNameEmpty() {
    if (document.getElementById('userName').value == "") {
        return 'UserName should not be empty';
    } else { return ""; }
}
function IsPasswordEmpty() {
    if (document.getElementById('pwd').value == "") {
        return 'Password  should not be empty';
    } else { return ""; }
}
//function IsEmailValid() {
//    if (document.getElementById('email').value.indexOf("@") = -1) {
//        return 'Please enter a valid email';
//    }else { return ""; }
//}

function IsValid() {
    var FirstNameEmptyMessage = IsFirstNameEmpty();
    var FirstNameInValidMessage = IsFirstNameInValid();
    var LastNameInValidMessage = IsLastNameInValid();
    var LastNameEmptyMessage = IsLastNameEmpty();
    var AddressEmptyMessage = IsAddressEmpty();
    var AgeValidMessage = IsAgeValid();
    var MobileEmptyMessage = IsMobileEmpty();
    var EmailEmptyMessage = IsEmailEmpty();
    var UserNameEmptyMessage = IsUserNameEmpty();
    var PasswordEmptyMessage = IsPasswordEmpty();
    //var EmailValidMessage = IsEmailValid();
    var FinalErrorMessage = "Errors:";
    
    if (FirstNameEmptyMessage != "")
        FinalErrorMessage += "\n" + FirstNameEmptyMessage;
    if (FirstNameInValidMessage != "")
        FinalErrorMessage += "\n" + FirstNameInValidMessage;
    if (LastNameInValidMessage != "")
        FinalErrorMessage += "\n" + LastNameInValidMessage;
    if (LastNameEmptyMessage != "")
        FinalErrorMessage += "\n" + LastNameEmptyMessage;
    if (AgeValidMessage != "")
        FinalErrorMessage += "\n" + AgeValidMessage;
    if ( EmailEmptyMessage != "")
        FinalErrorMessage += "\n" +  EmailEmptyMessage;
    //if ( EmailValidMessage != "")
    //    FinalErrorMessage += "\n" +  EmailValidMessage;
    if (AddressEmptyMessage != "")
        FinalErrorMessage += "\n" + AddressEmptyMessage;
    if (MobileEmptyMessage != "")
        FinalErrorMessage += "\n" + MobileEmptyMessage;
    if (UserNameEmptyMessage != "")
        FinalErrorMessage += "\n" + UserNameEmptyMessage;
    if (PasswordEmptyMessage != "")
        FinalErrorMessage += "\n" + PasswordEmptyMessage;
    if (FinalErrorMessage != "Errors:") {
        alert(FinalErrorMessage);
        return false;
    }
    else {
        return true;
    }
}
