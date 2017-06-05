namespace ColtraneXmlConverter
{
    using System;
    using System.IO;
    using System.Linq;
    using Ocas.Coltrane;

    internal static class Program
    {
        // Returns 0 for Success, 1 for failure
        public static int Main(string[] args)
        {
            var inputFile = args.FirstOrDefault() ?? "SAMPLE.xml";
            var outputFile = $"Output-{DateTime.Now:yyyyMMddHHmmss}.txt";
            try
            {
                if (!File.Exists(inputFile))
                {
                    Console.WriteLine($"File: '{inputFile}' does not exist");
                    return 1;
                }

                // Loads the file argument passed (relative to the Executible location)
                var collegeTransmission = CollegeTransmissionType.LoadFromFileAndValidate(inputFile);

                // Write to File
                WriteToFile(collegeTransmission, outputFile);

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString()); // Write Error to Console
                File.WriteAllText(outputFile, e.ToString()); // Write Error to File
                return 1;
            }
            finally
            {
                Console.WriteLine("---------------");
                Console.WriteLine("Done");
            }
        }

        public static void WriteToFile(CollegeTransmissionType collegeTransmission, string filename, bool append = false)
        {
            using (var fileStream = new StreamWriter(filename, append))
            {
                WriteColetrane(collegeTransmission, fileStream);
            }
        }

        public static void WriteColetrane(CollegeTransmissionType collegeTransmission, StreamWriter fileStream)
        {
            var nullMsg = "NOT PROVIDED";

            fileStream.WriteLine("-----------------------------------Header--------------------------------------");
            fileStream.WriteLine("CollegeCode: " +
                                 (collegeTransmission.Header.GetStringOrDefault(x => nameof(x.CollegeCode)) ?? nullMsg));
            fileStream.WriteLine("TransmissionDateTime: " +
                                 (collegeTransmission.Header.GetStringOrDefault(x => nameof(x.TransmissionDateTime)) ?? nullMsg));

            foreach (var application in collegeTransmission.Details)
            {
                fileStream.WriteLine("-----------------------------------Application.ApplicantIdentification---------");
                fileStream.WriteLine("AccountNumber: " + (application.ApplicantIdentification.GetStringOrDefault(x => nameof(x.AccountNumber)) ?? nullMsg));
                fileStream.WriteLine("ApplicationNumber: " + (application.ApplicantIdentification.GetStringOrDefault(x => nameof(x.ApplicationNumber)) ?? nullMsg));

                fileStream.WriteLine("-----------------------------------Applicant.Address---------------------------");
                fileStream.WriteLine("AddressStreet: " + (application.Applicant.Address.GetStringOrDefault(x => nameof(x.AddressStreet)) ?? nullMsg));
                fileStream.WriteLine("City: " + (application.Applicant.Address.GetStringOrDefault(x => nameof(x.City)) ?? nullMsg));
                fileStream.WriteLine("CountryCode: " + (application.Applicant.Address.GetStringOrDefault(x => nameof(x.CountryCode)) ?? nullMsg));
                fileStream.WriteLine("CountryName: " + (application.Applicant.Address.GetStringOrDefault(x => nameof(x.CountryName)) ?? nullMsg));
                fileStream.WriteLine("PostalCode: " + (application.Applicant.Address.GetStringOrDefault(x => nameof(x.PostalCode)) ?? nullMsg));
                fileStream.WriteLine("ProvinceState: " + (application.Applicant.Address.GetStringOrDefault(x => nameof(x.ProvinceState)) ?? nullMsg));

                fileStream.WriteLine("-----------------------------------Applicant.ApplicationInformation------------");
                fileStream.WriteLine("BasisOfAdmission: " + (application.Applicant.ApplicationInformation.GetStringOrDefault(x => nameof(x.BasisOfAdmission)) ?? nullMsg));
                fileStream.WriteLine("CurrentStudent: " + (application.Applicant.ApplicationInformation.GetStringOrDefault(x => nameof(x.CurrentStudent)) ?? nullMsg));
                fileStream.WriteLine("DateApplicationReceived: " + (application.Applicant.ApplicationInformation.GetStringOrDefault(x => nameof(x.DateApplicationReceived)) ?? nullMsg));
                fileStream.WriteLine("GradeStatus: " + (application.Applicant.ApplicationInformation.GetStringOrDefault(x => nameof(x.GradeStatus)) ?? nullMsg));
                fileStream.WriteLine("MethodOfApplication: " + (application.Applicant.ApplicationInformation.GetStringOrDefault(x => nameof(x.MethodOfApplication)) ?? nullMsg));

                fileStream.WriteLine("-----------------------------------Applicant.Demographic-----------------------");
                fileStream.WriteLine("Aboriginal: " + (application.Applicant.Demographic.GetStringOrDefault(x => nameof(x.Aboriginal)) ?? nullMsg));
                fileStream.WriteLine("AboriginalPersonIndicator: " + (application.Applicant.Demographic.GetStringOrDefault(x => nameof(x.AboriginalPersonIndicator)) ?? nullMsg));
                fileStream.WriteLine("AboriginalPersonStatus: " + (application.Applicant.Demographic.GetStringOrDefault(x => nameof(x.AboriginalPersonStatus)) ?? nullMsg));
                fileStream.WriteLine("BirthDate: " + (application.Applicant.Demographic.GetStringOrDefault(x => nameof(x.BirthDate)) ?? nullMsg));
                fileStream.WriteLine("CountryOfBirth: " + (application.Applicant.Demographic.GetStringOrDefault(x => nameof(x.CountryOfBirth)) ?? nullMsg));
                fileStream.WriteLine("CountryOfCitizen: " + (application.Applicant.Demographic.GetStringOrDefault(x => nameof(x.CountryOfCitizen)) ?? nullMsg));
                fileStream.WriteLine("FirstGeneration: " + (application.Applicant.Demographic.GetStringOrDefault(x => nameof(x.FirstGeneration)) ?? nullMsg));
                fileStream.WriteLine("FirstName: " + (application.Applicant.Demographic.GetStringOrDefault(x => nameof(x.FirstName)) ?? nullMsg));
                fileStream.WriteLine("Gender: " + (application.Applicant.Demographic.GetStringOrDefault(x => nameof(x.Gender)) ?? nullMsg));
                fileStream.WriteLine("LanguageOfCorrespondence: " + (application.Applicant.Demographic.GetStringOrDefault(x => nameof(x.LanguageOfCorrespondence)) ?? nullMsg));
                fileStream.WriteLine("LastName: " + (application.Applicant.Demographic.GetStringOrDefault(x => nameof(x.LastName)) ?? nullMsg));
                fileStream.WriteLine("MotherTongue: " + (application.Applicant.Demographic.GetStringOrDefault(x => nameof(x.MotherTongue)) ?? nullMsg));
                fileStream.WriteLine("PreferredName: " + (application.Applicant.Demographic.GetStringOrDefault(x => nameof(x.PreferredName)) ?? nullMsg));
                fileStream.WriteLine("PreviousLastName: " + (application.Applicant.Demographic.GetStringOrDefault(x => nameof(x.PreviousLastName)) ?? nullMsg));
                fileStream.WriteLine("SecondName: " + (application.Applicant.Demographic.GetStringOrDefault(x => nameof(x.SecondName)) ?? nullMsg));
                fileStream.WriteLine("SponsorAgency: " + (application.Applicant.Demographic.GetStringOrDefault(x => nameof(x.SponsorAgency)) ?? nullMsg));
                fileStream.WriteLine("StatusInCanada: " + (application.Applicant.Demographic.GetStringOrDefault(x => nameof(x.StatusInCanada)) ?? nullMsg));
                fileStream.WriteLine("Title: " + (application.Applicant.Demographic.GetStringOrDefault(x => nameof(x.Title)) ?? nullMsg));

                fileStream.WriteLine("-----------------------------------Applicant.Email-----------------------------");
                fileStream.WriteLine("EmailAddress: " + (application.Applicant.Email.GetStringOrDefault(x => nameof(x.EmailAddress)) ?? nullMsg));

                fileStream.WriteLine("-----------------------------------Applicant.InternationalAcademicData---------");
                fileStream.WriteLine("EvaluationGraduationDate: " + (application.Applicant.InternationalAcademicData.GetStringOrDefault(x => nameof(x.EvaluationGraduationDate)) ?? nullMsg));
                fileStream.WriteLine("SecondarySchoolEvaluation: " + (application.Applicant.InternationalAcademicData.GetStringOrDefault(x => nameof(x.SecondarySchoolEvaluation)) ?? nullMsg));

                fileStream.WriteLine("-----------------------------------Applicant.OntarioAcademicData---------------");
                fileStream.WriteLine("CommunityInvolvement: " + (application.Applicant.OntarioAcademicData.GetStringOrDefault(x => nameof(x.CommunityInvolvement)) ?? nullMsg));
                fileStream.WriteLine("GEDCompletionDate: " + (application.Applicant.OntarioAcademicData.GetStringOrDefault(x => nameof(x.GEDCompletionDate)) ?? nullMsg));
                fileStream.WriteLine("MidentCode: " + (application.Applicant.OntarioAcademicData.GetStringOrDefault(x => nameof(x.MidentCode)) ?? nullMsg));
                fileStream.WriteLine("OEN: " + (application.Applicant.OntarioAcademicData.GetStringOrDefault(x => nameof(x.OEN)) ?? nullMsg));
                fileStream.WriteLine("OENValid: " + (application.Applicant.OntarioAcademicData.GetStringOrDefault(x => nameof(x.OENValid)) ?? nullMsg));
                fileStream.WriteLine("OENValidDate: " + (application.Applicant.OntarioAcademicData.GetStringOrDefault(x => nameof(x.OENValidDate)) ?? nullMsg));
                fileStream.WriteLine("OSSCIssueDate: " + (application.Applicant.OntarioAcademicData.GetStringOrDefault(x => nameof(x.OSSCIssueDate)) ?? nullMsg));
                fileStream.WriteLine("OSSCToBeAchieved: " + (application.Applicant.OntarioAcademicData.GetStringOrDefault(x => nameof(x.OSSCToBeAchieved)) ?? nullMsg));
                fileStream.WriteLine("OSSDIssueDate: " + (application.Applicant.OntarioAcademicData.GetStringOrDefault(x => nameof(x.OSSDIssueDate)) ?? nullMsg));
                fileStream.WriteLine("OSSDToBeAchieved: " + (application.Applicant.OntarioAcademicData.GetStringOrDefault(x => nameof(x.OSSDToBeAchieved)) ?? nullMsg));
                fileStream.WriteLine("SHSMCode: " + (application.Applicant.OntarioAcademicData.GetStringOrDefault(x => nameof(x.SHSMCode)) ?? nullMsg));
                fileStream.WriteLine("SSLiteracyTest: " + (application.Applicant.OntarioAcademicData.GetStringOrDefault(x => nameof(x.SSLiteracyTest)) ?? nullMsg));

                fileStream.WriteLine("-----------------------------------Applicant.Phones.CellPhone------------------");
                fileStream.WriteLine("AreaCode: " + (application.Applicant.Phones.CellPhone.GetStringOrDefault(x => nameof(x.AreaCode)) ?? nullMsg));
                fileStream.WriteLine("Phone: " + (application.Applicant.Phones.CellPhone.GetStringOrDefault(x => nameof(x.Phone)) ?? nullMsg));

                fileStream.WriteLine("-----------------------------------Applicant.Phones.PrimaryPhone---------------");
                fileStream.WriteLine("AreaCode: " + (application.Applicant.Phones.PrimaryPhone.GetStringOrDefault(x => nameof(x.AreaCode)) ?? nullMsg));
                fileStream.WriteLine("Phone: " + (application.Applicant.Phones.PrimaryPhone.GetStringOrDefault(x => nameof(x.Phone)) ?? nullMsg));

                fileStream.WriteLine("-----------------------------------Applicant-----------------------------------");
                fileStream.WriteLine("TransactionCode: " + (application.Applicant.GetStringOrDefault(x => nameof(x.TransactionCode)) ?? nullMsg));
                fileStream.WriteLine("TransactionDateTime: " + (application.Applicant.GetStringOrDefault(x => nameof(x.TransactionDateTime)) ?? nullMsg));
                fileStream.WriteLine("TransactionType1: " + (application.Applicant.GetStringOrDefault(x => nameof(x.TransactionType1)) ?? nullMsg));

                foreach (var cred in application.Credentials.CredentialTestScores)
                {
                    fileStream.WriteLine("-----------------------------------CredentialTestScore-------------------------");
                    fileStream.WriteLine("GradeEquivalent: " + (cred.GetStringOrDefault(x => nameof(x.GradeEquivalent)) ?? nullMsg));
                    fileStream.WriteLine("NormingGroup: " + (cred.GetStringOrDefault(x => nameof(x.NormingGroup)) ?? nullMsg));
                    fileStream.WriteLine("NumberCorrect: " + (cred.GetStringOrDefault(x => nameof(x.NumberCorrect)) ?? nullMsg));
                    fileStream.WriteLine("NumberOfItems: " + (cred.GetStringOrDefault(x => nameof(x.NumberOfItems)) ?? nullMsg));
                    fileStream.WriteLine("Percentile: " + (cred.GetStringOrDefault(x => nameof(x.Percentile)) ?? nullMsg));
                    fileStream.WriteLine("StanineCode: " + (cred.GetStringOrDefault(x => nameof(x.StanineCode)) ?? nullMsg));
                    fileStream.WriteLine("SubTestCode: " + (cred.GetStringOrDefault(x => nameof(x.SubTestCode)) ?? nullMsg));
                    fileStream.WriteLine("TestCentre: " + (cred.GetStringOrDefault(x => nameof(x.TestCentre)) ?? nullMsg));
                    fileStream.WriteLine("TestCentreCode: " + (cred.GetStringOrDefault(x => nameof(x.TestCentreCode)) ?? nullMsg));
                    fileStream.WriteLine("TestCode: " + (cred.GetStringOrDefault(x => nameof(x.TestCode)) ?? nullMsg));
                    fileStream.WriteLine("TestDate: " + (cred.GetStringOrDefault(x => nameof(x.TestDate)) ?? nullMsg));
                    fileStream.WriteLine("TransactionCode: " + (cred.GetStringOrDefault(x => nameof(x.TransactionCode)) ?? nullMsg));
                    fileStream.WriteLine("TransactionDateTime: " + (cred.GetStringOrDefault(x => nameof(x.TransactionDateTime)) ?? nullMsg));
                    fileStream.WriteLine("TransactionType1: " + (cred.GetStringOrDefault(x => nameof(x.TransactionType1)) ?? nullMsg));
                }

                foreach (var grade in application.Credentials.InternationalGrades)
                {
                    fileStream.WriteLine("-----------------------------------InternationalGrade--------------------------");
                    fileStream.WriteLine("CompletionDate: " + (grade.GetStringOrDefault(x => nameof(x.CompletionDate)) ?? nullMsg));
                    fileStream.WriteLine("CourseCode: " + (grade.GetStringOrDefault(x => nameof(x.CourseCode)) ?? nullMsg));
                    fileStream.WriteLine("CreditValue: " + (grade.GetStringOrDefault(x => nameof(x.CreditValue)) ?? nullMsg));
                    fileStream.WriteLine("Grade: " + (grade.GetStringOrDefault(x => nameof(x.Grade)) ?? nullMsg));
                    fileStream.WriteLine("TransactionCode: " + (grade.GetStringOrDefault(x => nameof(x.TransactionCode)) ?? nullMsg));
                    fileStream.WriteLine("TransactionDateTime: " + (grade.GetStringOrDefault(x => nameof(x.TransactionDateTime)) ?? nullMsg));
                    fileStream.WriteLine("TransactionType1: " + (grade.GetStringOrDefault(x => nameof(x.TransactionType1)) ?? nullMsg));
                }

                foreach (var activity in application.Credentials.NonFullTimeStudentActivity)
                {
                    fileStream.WriteLine("-----------------------------------NonFullTimeStudentActivity------------------");
                    fileStream.WriteLine("ActivityType: " + (activity.GetStringOrDefault(x => nameof(x.ActivityType)) ?? nullMsg));
                    fileStream.WriteLine("Employer: " + (activity.GetStringOrDefault(x => nameof(x.Employer)) ?? nullMsg));
                    fileStream.WriteLine("EndDate: " + (activity.GetStringOrDefault(x => nameof(x.EndDate)) ?? nullMsg));
                    fileStream.WriteLine("LifeSequenceNumber: " + (activity.GetStringOrDefault(x => nameof(x.LifeSequenceNumber)) ?? nullMsg));
                    fileStream.WriteLine("StartDate: " + (activity.GetStringOrDefault(x => nameof(x.StartDate)) ?? nullMsg));
                    fileStream.WriteLine("TransactionCode: " + (activity.GetStringOrDefault(x => nameof(x.TransactionCode)) ?? nullMsg));
                    fileStream.WriteLine("TransactionDateTime: " + (activity.GetStringOrDefault(x => nameof(x.TransactionDateTime)) ?? nullMsg));
                    fileStream.WriteLine("TransactionType1: " + (activity.GetStringOrDefault(x => nameof(x.TransactionType1)) ?? nullMsg));
                }

                foreach (var hsGrade in application.Credentials.OntarioHighSchoolGrades)
                {
                    fileStream.WriteLine("-----------------------------------OntarioHighSchoolGrade----------------------");
                    fileStream.WriteLine("CompletionDate: " + (hsGrade.GetStringOrDefault(x => nameof(x.CompletionDate)) ?? nullMsg));
                    fileStream.WriteLine("CourseCode: " + (hsGrade.GetStringOrDefault(x => nameof(x.CourseCode)) ?? nullMsg));
                    fileStream.WriteLine("CourseDelivery: " + (hsGrade.GetStringOrDefault(x => nameof(x.CourseDelivery)) ?? nullMsg));
                    fileStream.WriteLine("CourseSource: " + (hsGrade.GetStringOrDefault(x => nameof(x.CourseSource)) ?? nullMsg));
                    fileStream.WriteLine("CourseStatus: " + (hsGrade.GetStringOrDefault(x => nameof(x.CourseStatus)) ?? nullMsg));
                    fileStream.WriteLine("CourseType: " + (hsGrade.GetStringOrDefault(x => nameof(x.CourseType)) ?? nullMsg));
                    fileStream.WriteLine("CreditValue: " + (hsGrade.GetStringOrDefault(x => nameof(x.CreditValue)) ?? nullMsg));
                    fileStream.WriteLine("EditException: " + (hsGrade.GetStringOrDefault(x => nameof(x.EditException)) ?? nullMsg));
                    fileStream.WriteLine("Grade: " + (hsGrade.GetStringOrDefault(x => nameof(x.Grade)) ?? nullMsg));
                    fileStream.WriteLine("GradeType: " + (hsGrade.GetStringOrDefault(x => nameof(x.GradeType)) ?? nullMsg));
                    fileStream.WriteLine("MidentCode: " + (hsGrade.GetStringOrDefault(x => nameof(x.MidentCode)) ?? nullMsg));

                    foreach (var note in hsGrade.Note)
                    {
                        fileStream.WriteLine("-----------------------------------Note----------------------------------------");
                        fileStream.WriteLine("NoteType1: " + (note.GetStringOrDefault(x => nameof(x.NoteType1)) ?? nullMsg));
                    }

                    fileStream.WriteLine("TransactionCode: " + (hsGrade.GetStringOrDefault(x => nameof(x.TransactionCode)) ?? nullMsg));
                    fileStream.WriteLine("TransactionDateTime: " + (hsGrade.GetStringOrDefault(x => nameof(x.TransactionDateTime)) ?? nullMsg));
                    fileStream.WriteLine("TransactionType1: " + (hsGrade.GetStringOrDefault(x => nameof(x.TransactionType1)) ?? nullMsg));
                }

                foreach (var education in application.Credentials.SelfDeclaredEducation)
                {
                    fileStream.WriteLine("-----------------------------------SelfDeclaredEducation-----------------------");
                    fileStream.WriteLine("City: " + (education.GetStringOrDefault(x => nameof(x.City)) ?? nullMsg));
                    fileStream.WriteLine("CountryCode: " + (education.GetStringOrDefault(x => nameof(x.CountryCode)) ?? nullMsg));
                    fileStream.WriteLine("CredentialCode: " + (education.GetStringOrDefault(x => nameof(x.CredentialCode)) ?? nullMsg));
                    fileStream.WriteLine("EducationSequenceNumber: " + (education.GetStringOrDefault(x => nameof(x.EducationSequenceNumber)) ?? nullMsg));
                    fileStream.WriteLine("EndDate: " + (education.GetStringOrDefault(x => nameof(x.EndDate)) ?? nullMsg));
                    fileStream.WriteLine("InstitutionCode: " + (education.GetStringOrDefault(x => nameof(x.InstitutionCode)) ?? nullMsg));
                    fileStream.WriteLine("InstitutionName: " + (education.GetStringOrDefault(x => nameof(x.InstitutionName)) ?? nullMsg));
                    fileStream.WriteLine("LegalFirstName: " + (education.GetStringOrDefault(x => nameof(x.LegalFirstName)) ?? nullMsg));
                    fileStream.WriteLine("LegalLastName: " + (education.GetStringOrDefault(x => nameof(x.LegalLastName)) ?? nullMsg));
                    fileStream.WriteLine("LevelAchieved: " + (education.GetStringOrDefault(x => nameof(x.LevelAchieved)) ?? nullMsg));
                    fileStream.WriteLine("ProgramName: " + (education.GetStringOrDefault(x => nameof(x.ProgramName)) ?? nullMsg));
                    fileStream.WriteLine("ProvinceState: " + (education.GetStringOrDefault(x => nameof(x.ProvinceState)) ?? nullMsg));
                    fileStream.WriteLine("StartDate: " + (education.GetStringOrDefault(x => nameof(x.StartDate)) ?? nullMsg));
                    fileStream.WriteLine("TransactionCode: " + (education.GetStringOrDefault(x => nameof(x.TransactionCode)) ?? nullMsg));
                    fileStream.WriteLine("TransactionDateTime: " + (education.GetStringOrDefault(x => nameof(x.TransactionDateTime)) ?? nullMsg));
                    fileStream.WriteLine("TransactionType1: " + (education.GetStringOrDefault(x => nameof(x.TransactionType1)) ?? nullMsg));
                }

                foreach (var doc in application.Credentials.SupportingDocuments)
                {
                    fileStream.WriteLine("-----------------------------------SupportingDocument--------------------------");
                    fileStream.WriteLine("CountryCode: " + (doc.GetStringOrDefault(x => nameof(x.CountryCode)) ?? nullMsg));
                    fileStream.WriteLine("CredentialCode: " + (doc.GetStringOrDefault(x => nameof(x.CredentialCode)) ?? nullMsg));
                    fileStream.WriteLine("DocumentCode: " + (doc.GetStringOrDefault(x => nameof(x.DocumentCode)) ?? nullMsg));
                    fileStream.WriteLine("DocumentLink: " + (doc.GetStringOrDefault(x => nameof(x.DocumentLink)) ?? nullMsg));
                    fileStream.WriteLine("DocumentNumber: " + (doc.GetStringOrDefault(x => nameof(x.DocumentNumber)) ?? nullMsg));
                    fileStream.WriteLine("DocumentStatus: " + (doc.GetStringOrDefault(x => nameof(x.DocumentStatus)) ?? nullMsg));
                    fileStream.WriteLine("DriverLicenceClass: " + (doc.GetStringOrDefault(x => nameof(x.DriverLicenceClass)) ?? nullMsg));
                    fileStream.WriteLine("DriverLicenceType: " + (doc.GetStringOrDefault(x => nameof(x.DriverLicenceType)) ?? nullMsg));
                    fileStream.WriteLine("ExpiryDate: " + (doc.GetStringOrDefault(x => nameof(x.ExpiryDate)) ?? nullMsg));
                    fileStream.WriteLine("GraduationDate: " + (doc.GetStringOrDefault(x => nameof(x.GraduationDate)) ?? nullMsg));
                    fileStream.WriteLine("ImmigrationLandingDate: " + (doc.GetStringOrDefault(x => nameof(x.ImmigrationLandingDate)) ?? nullMsg));
                    fileStream.WriteLine("ImmigrationLandingSignature: " + (doc.GetStringOrDefault(x => nameof(x.ImmigrationLandingSignature)) ?? nullMsg));
                    fileStream.WriteLine("InstitutionAccreditation: " + (doc.GetStringOrDefault(x => nameof(x.InstitutionAccreditation)) ?? nullMsg));
                    fileStream.WriteLine("InstitutionCode: " + (doc.GetStringOrDefault(x => nameof(x.InstitutionCode)) ?? nullMsg));
                    fileStream.WriteLine("InstitutionName: " + (doc.GetStringOrDefault(x => nameof(x.InstitutionName)) ?? nullMsg));
                    fileStream.WriteLine("InstitutionSequenceNumber: " + (doc.GetStringOrDefault(x => nameof(x.InstitutionSequenceNumber)) ?? nullMsg));
                    fileStream.WriteLine("InstitutionTypeCode: " + (doc.GetStringOrDefault(x => nameof(x.InstitutionTypeCode)) ?? nullMsg));
                    fileStream.WriteLine("IssueDate: " + (doc.GetStringOrDefault(x => nameof(x.IssueDate)) ?? nullMsg));
                    fileStream.WriteLine("IssuingAgency: " + (doc.GetStringOrDefault(x => nameof(x.IssuingAgency)) ?? nullMsg));
                    fileStream.WriteLine("KeyDate: " + (doc.GetStringOrDefault(x => nameof(x.KeyDate)) ?? nullMsg));
                    fileStream.WriteLine("Level: " + (doc.GetStringOrDefault(x => nameof(x.Level)) ?? nullMsg));
                    fileStream.WriteLine("LevelAchieved: " + (doc.GetStringOrDefault(x => nameof(x.LevelAchieved)) ?? nullMsg));
                    fileStream.WriteLine("OfficialNonOfficial: " + (doc.GetStringOrDefault(x => nameof(x.OfficialNonOfficial)) ?? nullMsg));
                    fileStream.WriteLine("OriginalCopy: " + (doc.GetStringOrDefault(x => nameof(x.OriginalCopy)) ?? nullMsg));
                    fileStream.WriteLine("ProgramName: " + (doc.GetStringOrDefault(x => nameof(x.ProgramName)) ?? nullMsg));
                    fileStream.WriteLine("ProvinceState: " + (doc.GetStringOrDefault(x => nameof(x.ProvinceState)) ?? nullMsg));
                    fileStream.WriteLine("TransactionCode: " + (doc.GetStringOrDefault(x => nameof(x.TransactionCode)) ?? nullMsg));
                    fileStream.WriteLine("TransactionDateTime: " + (doc.GetStringOrDefault(x => nameof(x.TransactionDateTime)) ?? nullMsg));
                    fileStream.WriteLine("TransactionType1: " + (doc.GetStringOrDefault(x => nameof(x.TransactionType1)) ?? nullMsg));
                }

                foreach (var choice in application.ProgramChoice)
                {
                    fileStream.WriteLine("-----------------------------------ProgramChoice-------------------------------");
                    fileStream.WriteLine("CampusCode: " + (choice.GetStringOrDefault(x => nameof(x.CampusCode)) ?? nullMsg));
                    fileStream.WriteLine("ChoiceNumber: " + (choice.GetStringOrDefault(x => nameof(x.ChoiceNumber)) ?? nullMsg));
                    fileStream.WriteLine("DateDecisionReceived: " + (choice.GetStringOrDefault(x => nameof(x.DateDecisionReceived)) ?? nullMsg));
                    fileStream.WriteLine("FullPartTime: " + (choice.GetStringOrDefault(x => nameof(x.FullPartTime)) ?? nullMsg));
                    fileStream.WriteLine("PreviousAppliedYear: " + (choice.GetStringOrDefault(x => nameof(x.PreviousAppliedYear)) ?? nullMsg));
                    fileStream.WriteLine("PreviousAttendedYear: " + (choice.GetStringOrDefault(x => nameof(x.PreviousAttendedYear)) ?? nullMsg));
                    fileStream.WriteLine("Program: " + (choice.GetStringOrDefault(x => nameof(x.Program)) ?? nullMsg));
                    fileStream.WriteLine("Semester: " + (choice.GetStringOrDefault(x => nameof(x.Semester)) ?? nullMsg));
                    fileStream.WriteLine("StartDate: " + (choice.GetStringOrDefault(x => nameof(x.StartDate)) ?? nullMsg));
                    fileStream.WriteLine("TransactionCode: " + (choice.GetStringOrDefault(x => nameof(x.TransactionCode)) ?? nullMsg));
                    fileStream.WriteLine("TransactionDateTime: " + (choice.GetStringOrDefault(x => nameof(x.TransactionDateTime)) ?? nullMsg));
                    fileStream.WriteLine("TransactionType1: " + (choice.GetStringOrDefault(x => nameof(x.TransactionType1)) ?? nullMsg));
                }
            }

            fileStream.WriteLine("-----------------------------------Trailer-----------------------------------------");
            fileStream.WriteLine("CountAC: " + (collegeTransmission.Trailer.GetStringOrDefault(x => nameof(x.CountAC)) ?? nullMsg));
            fileStream.WriteLine("CountCC: " + (collegeTransmission.Trailer.GetStringOrDefault(x => nameof(x.CountCC)) ?? nullMsg));
            fileStream.WriteLine("CountEC: " + (collegeTransmission.Trailer.GetStringOrDefault(x => nameof(x.CountEC)) ?? nullMsg));
            fileStream.WriteLine("CountGC: " + (collegeTransmission.Trailer.GetStringOrDefault(x => nameof(x.CountGC)) ?? nullMsg));
            fileStream.WriteLine("CountIC: " + (collegeTransmission.Trailer.GetStringOrDefault(x => nameof(x.CountIC)) ?? nullMsg));
            fileStream.WriteLine("CountNC: " + (collegeTransmission.Trailer.GetStringOrDefault(x => nameof(x.CountNC)) ?? nullMsg));
            fileStream.WriteLine("CountSC: " + (collegeTransmission.Trailer.GetStringOrDefault(x => nameof(x.CountSC)) ?? nullMsg));
            fileStream.WriteLine("CountTC: " + (collegeTransmission.Trailer.GetStringOrDefault(x => nameof(x.CountTC)) ?? nullMsg));
        }
    }
}
