BEGIN TRY
    BEGIN TRAN
    -- Set an appropriate and descriptive error message for your script
    DECLARE @ErrorMessage NVARCHAR(255) = 'Test failed. Transaction rolled back.';
    DECLARE @TestResult INT;

    -- Your Script Goes Here

    -- Your measurable test script
    -- Assign 1 to @TestResult if the test passes, 0 if it fails
    -- Example: 
    -- IF [YourTestCondition] = [ExpectedValue]
    --    SET @TestResult = 1;
    -- ELSE
    --    SET @TestResult = 0;

    -- Check the test result
    IF @TestResult = 1
    BEGIN
        -- If the test passes, commit the transaction
        COMMIT;
    END
    ELSE
    BEGIN
        -- If the test fails, roll back the transaction and raise an error
        ROLLBACK;
        THROW 50000, @ErrorMessage, 1;
    END
END TRY
BEGIN CATCH
    -- Handle any SQL exceptions here
    -- Roll back the transaction and re-throw the exception
    IF @@TRANCOUNT > 0
        ROLLBACK;

    THROW;
END CATCH;
