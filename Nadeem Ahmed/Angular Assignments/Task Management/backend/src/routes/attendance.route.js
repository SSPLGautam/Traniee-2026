const express = require("express");
const authenticate = require("../middlewares/auth.middleware");
const authorize = require("../middlewares/role.middleware");
const { markAttendance, getMyAttendance, getAllAttendance } = require("../controllers/attendance.controller");


const router = express.Router();

router.post(
    "/mark",
    authenticate,
    authorize("Employee"),
    markAttendance
);

router.get(
    "/my",
    authenticate,
    authorize("Employee"),
    getMyAttendance
);

router.get(
    "/",
    authenticate,
    authorize("Admin", "Manager"),
    getAllAttendance
);

module.exports = router;