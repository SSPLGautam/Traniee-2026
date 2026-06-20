const { Attendance, User, Leave } = require("../models");

const createAttendance = async (data) => {

    return await Attendance.create(data);

};

const findTodayAttendance = async (
    employeeId,
    attendanceDate
) => {

    return await Attendance.findOne({

        where: {
            employeeId,
            attendanceDate
        }

    });

};


const getMyAttendance = async (
    employeeId
) => {

    return await Attendance.findAll({

        where: {
            employeeId
        },

        order: [
            ["attendanceDate", "DESC"]
        ]

    });

};

const getAllAttendance =
async () => {

  return await Attendance
    .findAll({

      include: [

        {
          model: User,
          as: "Employee",

          attributes: [
            "id",
            "username",
            "email"
          ]
        }

      ],

      order: [
        ["attendanceDate", "DESC"]
      ]

    });

};
module.exports = {

    createAttendance,

    findTodayAttendance,

    getMyAttendance,

    getAllAttendance

};