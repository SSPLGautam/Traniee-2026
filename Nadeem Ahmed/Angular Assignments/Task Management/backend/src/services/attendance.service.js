const attendanceRepository = require("../repositories/attendance.repository");

const markAttendance =
    async (employeeId) => {

        const today =
            new Date()
                .toISOString()
                .split("T")[0];

        const attendance =
            await attendanceRepository
                .findTodayAttendance(
                    employeeId,
                    today
                );

        if (attendance) {

            if (
                attendance.status ===
                "Present"
            ) {

                throw new Error(
                    "Attendance already marked"
                );

            }

            attendance.status =
                "Present";

            attendance.attendanceTime =
                new Date();

            return await attendance.save();

        }

        return await attendanceRepository
            .createAttendance({

                employeeId,

                attendanceDate:
                    today,

                attendanceTime:
                    new Date(),

                status:
                    "Present"

            });

    }

const getAllAttendance =
    async () => {

        return await attendanceRepository
            .getAllAttendance();

    };

module.exports = {
    markAttendance,
    getAllAttendance
}