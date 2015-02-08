var COMMON = COMMON || {};
COMMON = {
    closeGlobalMessageAlert: function () {
        $("#globalMessageParent").addClass("hidden");

    },
    showGlobalMessageAlert: function (message) {
        $("#globalMessage").html(message);
        $("#globalMessageParent").removeClass("hidden");
    },
    openOfflineConn: function () {
        var db = new Dexie("UserDB");
        db.version(1).stores({ users: "Key,Id,FirstName,LastName" });
        db.open(); // Aft
        return db;
    }
}



