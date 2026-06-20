const express = require('express');

const authenticate = require("../middlewares/auth.middleware");

const { createLeave, getAllLeaves, getMyLeaves, getManagerLeaves, updateLeaveStatus } = require("../controllers/leave.controller");
const authorize = require('../middlewares/role.middleware');




const router = express.Router();

router.post("/", authenticate, createLeave);

router.get("/", authenticate, getAllLeaves);

router.get("/my-leaves", authenticate, getMyLeaves);

router.get("/manager", authenticate, authorize("Manager"), getManagerLeaves);

router.put("/:id/status", authenticate, authorize("Manager"), updateLeaveStatus);

router.get("/", authenticate, authorize("Admin"), getAllLeaves);



module.exports = router;