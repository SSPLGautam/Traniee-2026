const cron = require("node-cron");

const { User, Leave, Attendance } = require("../models");

const { Op } = require("sequelize");

cron.schedule(

    "0 0 * * *",

    async () => {

        console.log("Attendance Cron Running...");

        const today =
            new Date()
                .toISOString()
                .split("T")[0];

        const employees =
            await User.findAll({

                where: {

                    roleId: 3

                }

            });

        for (const employee of employees) {

            const exists =
                await Attendance.findOne({

                    where: {

                        employeeId:
                            employee.id,

                        attendanceDate:
                            today

                    }

                });

            if (exists) {
                continue;
            }

            const leave =
                await Leave.findOne({

                    where: {

                        employeeId:
                            employee.id,

                        status:
                            "Approved",

                        fromDate: {
                            [Op.lte]: today
                        },

                        toDate: {
                            [Op.gte]: today
                        }

                    }

                });

            await Attendance.create({

                employeeId:
                    employee.id,

                attendanceDate:
                    today,

                attendanceTime:
                    null,

                status:
                    leave
                        ? "Leave"
                        : "Absent"

            });
            console.log(
                "Attendance Created Successfully"
            );
        }

    }
);