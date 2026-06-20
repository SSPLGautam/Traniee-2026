const leaveService = require("../services/leave.service");



const createLeave =
    async (req, res) => {

        try {

            const leave =
                await leaveService
                    .createLeave({

                        ...req.body,

                        employeeId:
                            req.user.id

                    });

            return res.status(201)
                .json({

                    success: true,

                    data: leave

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

const getAllLeaves =
    async (req, res) => {

        try {

            const leaves =
                await leaveService
                    .getAllLeaves();

            return res.status(200)
                .json({

                    success: true,

                    data: leaves

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

const getManagerLeaves =
    async (req, res) => {

        try {

            const leaves =
                await leaveService
                    .getManagerLeaves(
                        req.user.id
                    );

            return res.status(200)
                .json({

                    success: true,

                    data: leaves

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

const updateLeaveStatus =
    async (req, res) => {

        try {

            const leave =
                await leaveService
                    .updateLeaveStatus(

                        req.params.id,

                        req.body.status

                    );

            return res.status(200)
                .json({

                    success: true,

                    data: leave

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

const getMyLeaves = async (req, res) => {

    try {

        const leaves =
            await leaveService.getMyLeaves(
                req.user.id
            );

        return res.status(200).json({

            success: true,

            data: leaves

        });

    } catch (error) {

        return res.status(500).json({

            success: false,

            message: error.message

        });

    }

};

module.exports = {
    createLeave,
    getAllLeaves,
    getMyLeaves,
    getManagerLeaves,
    getMyLeaves,
    updateLeaveStatus
};