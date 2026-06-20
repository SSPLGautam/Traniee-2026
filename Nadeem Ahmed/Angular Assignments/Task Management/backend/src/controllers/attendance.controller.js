const attendanceService = require("../services/attendance.service");

const markAttendance =
    async (req, res) => {

        try {

            const attendance =
                await attendanceService
                    .markAttendance(
                        req.user.id
                    );

            return res.status(200)
                .json({

                    success: true,

                    data: attendance

                });

        } catch (error) {

            return res.status(500)
                .json({

                    success: false,

                    message: error.message

                });

        }

    };

const getMyAttendance =
    async (req, res) => {

        try {

            const attendance =
                await attendanceService
                    .getMyAttendance(
                        req.user.id
                    );

            return res.status(200)
                .json({

                    success: true,

                    data: attendance

                });

        } catch (error) {

            return res.status(500)
                .json({

                    success: false,

                    message: error.message

                });

        }

    };

const getAllAttendance =
async (req, res) => {

  try {

    const attendance =
      await attendanceService
        .getAllAttendance();

    return res.status(200)
      .json({

        success: true,

        data: attendance

      });

  } catch (error) {

    return res.status(500)
      .json({

        success: false,

        message:
          error.message

      });

  }

};

module.exports = {

    markAttendance,

    getMyAttendance,

    getAllAttendance

};