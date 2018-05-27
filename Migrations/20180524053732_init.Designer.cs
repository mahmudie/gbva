using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using rmc.Models;

namespace rmc.Migrations
{
    [DbContext(typeof(rmsContext))]
    [Migration("20180524053732_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("rmc.Models.AgencyType", b =>
                {
                    b.Property<int>("AgencyTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AgencyType1")
                        .HasColumnName("AgencyType")
                        .HasMaxLength(100);

                    b.HasKey("AgencyTypeId");

                    b.ToTable("AgencyType");
                });

            modelBuilder.Entity("rmc.Models.Authorization", b =>
                {
                    b.Property<int>("AuthorizationId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("GbvCaseId")
                        .HasColumnName("gbvCaseId");

                    b.Property<DateTime?>("InsertDate")
                        .HasColumnName("insertDate")
                        .HasColumnType("date");

                    b.Property<bool?>("IsSigned")
                        .HasColumnName("isSigned");

                    b.Property<bool?>("Islessthan18")
                        .HasColumnName("islessthan18");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnName("lastUpdate")
                        .HasColumnType("date");

                    b.Property<string>("UserName")
                        .HasColumnName("userName")
                        .HasMaxLength(50);

                    b.HasKey("AuthorizationId");

                    b.HasIndex("GbvCaseId");

                    b.ToTable("Authorization");
                });

            modelBuilder.Entity("rmc.Models.AuthorizationSub", b =>
                {
                    b.Property<int>("AuthorizationSubId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Authorization_subId");

                    b.Property<string>("AgencyName")
                        .HasMaxLength(100);

                    b.Property<int?>("AgencyTypeId");

                    b.Property<int?>("AuthorizationId");

                    b.Property<string>("Comment")
                        .HasMaxLength(250);

                    b.HasKey("AuthorizationSubId");

                    b.HasIndex("AgencyTypeId");

                    b.HasIndex("AuthorizationId");

                    b.ToTable("Authorization_sub");
                });

            modelBuilder.Entity("rmc.Models.Consent", b =>
                {
                    b.Property<int>("ConsentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("consentId");

                    b.Property<int>("BloodExams")
                        .HasColumnName("bloodExams");

                    b.Property<Guid>("GbvCaseId")
                        .HasColumnName("gbvCaseId");

                    b.Property<DateTime?>("InsertDate")
                        .HasColumnName("insertDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnName("lastUpdate")
                        .HasColumnType("date");

                    b.Property<int>("OtherExams")
                        .HasColumnName("otherExams");

                    b.Property<int>("PelvicExams")
                        .HasColumnName("pelvicExams");

                    b.Property<int>("PhysicalExams")
                        .HasColumnName("physicalExams");

                    b.Property<int>("ProvisionOfInfo")
                        .HasColumnName("provisionOfInfo");

                    b.Property<int>("SpeculumExams")
                        .HasColumnName("speculumExams");

                    b.Property<string>("UserName")
                        .HasColumnName("userName")
                        .HasMaxLength(50);

                    b.HasKey("ConsentId");

                    b.HasIndex("GbvCaseId");

                    b.ToTable("consent");
                });

            modelBuilder.Entity("rmc.Models.Districts", b =>
                {
                    b.Property<string>("DistCode")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("DistName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("DistNameDari")
                        .HasMaxLength(50);

                    b.Property<string>("DistNamePashto")
                        .HasMaxLength(255);

                    b.Property<string>("ProvCode")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("DistCode")
                        .HasName("PK_Districts");

                    b.HasIndex("ProvCode");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("rmc.Models.FacilityInfo", b =>
                {
                    b.Property<int>("FacilityId")
                        .HasColumnName("FacilityID");

                    b.Property<string>("ActiveStatus")
                        .HasMaxLength(10);

                    b.Property<DateTime?>("DateEstablished")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("DistCode")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FacilityName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("FacilityNameDari")
                        .HasMaxLength(255);

                    b.Property<string>("FacilityNamePashto")
                        .HasMaxLength(255);

                    b.Property<int>("FacilityType");

                    b.Property<double?>("Gpslattitude")
                        .HasColumnName("GPSLattitude");

                    b.Property<double?>("Gpslongtitude")
                        .HasColumnName("GPSLongtitude");

                    b.Property<string>("Implementer")
                        .IsRequired();

                    b.Property<double?>("Lat")
                        .HasColumnName("LAT");

                    b.Property<string>("Location")
                        .HasMaxLength(100);

                    b.Property<string>("LocationDari")
                        .HasMaxLength(100);

                    b.Property<string>("LocationPashto")
                        .HasMaxLength(100);

                    b.Property<double?>("Lon")
                        .HasColumnName("LON");

                    b.Property<string>("SubImplementer")
                        .HasMaxLength(255);

                    b.Property<string>("ViliCode")
                        .HasMaxLength(255);

                    b.HasKey("FacilityId")
                        .HasName("PK_FacilityInfo");

                    b.HasIndex("DistCode");

                    b.ToTable("FacilityInfo");
                });

            modelBuilder.Entity("rmc.Models.GbvCase", b =>
                {
                    b.Property<Guid>("GbvCaseId")
                        .HasColumnName("gbvCaseId");

                    b.Property<string>("Address")
                        .HasColumnName("address")
                        .HasMaxLength(255);

                    b.Property<int?>("Age")
                        .HasColumnName("age");

                    b.Property<int?>("HospitalId")
                        .HasColumnName("hospitalId");

                    b.Property<string>("IncCode")
                        .HasColumnName("incCode")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("InsertDate")
                        .HasColumnName("insertDate")
                        .HasColumnType("date");

                    b.Property<string>("ItrCode")
                        .HasColumnName("itrCode")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("date");

                    b.Property<int?>("MaritalStatus")
                        .HasColumnName("maritalStatus");

                    b.Property<string>("PatientFatherName")
                        .HasColumnName("patientFatherName")
                        .HasMaxLength(255);

                    b.Property<string>("PatientName")
                        .HasColumnName("patientName")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("RefDate")
                        .HasColumnName("refDate")
                        .HasColumnType("date");

                    b.Property<TimeSpan?>("RefTime")
                        .HasColumnName("refTime");

                    b.Property<int?>("RegNo")
                        .HasColumnName("regNo");

                    b.Property<string>("RptCode")
                        .HasColumnName("rptCode")
                        .HasMaxLength(50);

                    b.Property<int?>("Sex")
                        .HasColumnName("sex");

                    b.Property<string>("UserName")
                        .HasColumnName("userName")
                        .HasMaxLength(50);

                    b.HasKey("GbvCaseId");

                    b.HasIndex("HospitalId");

                    b.ToTable("gbvCase");
                });

            modelBuilder.Entity("rmc.Models.IntakeInfo", b =>
                {
                    b.Property<int>("IntakeInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("intakeInfoId");

                    b.Property<int?>("AgeGroup")
                        .HasColumnName("ageGroup");

                    b.Property<int?>("Aihrc")
                        .HasColumnName("AIHRC");

                    b.Property<int?>("Appointment")
                        .HasColumnName("appointment");

                    b.Property<int?>("BackHome")
                        .HasColumnName("backHome");

                    b.Property<int?>("ConsentTaken")
                        .HasColumnName("consentTaken");

                    b.Property<int?>("EvidenceCollected")
                        .HasColumnName("evidenceCollected");

                    b.Property<int?>("EvidenceOfPregnancy")
                        .HasColumnName("evidenceOfPregnancy");

                    b.Property<DateTime?>("ExamDate")
                        .HasColumnName("examDate")
                        .HasColumnType("date");

                    b.Property<TimeSpan?>("ExamTime")
                        .HasColumnName("examTime");

                    b.Property<int?>("ExaminedForAids")
                        .HasColumnName("examinedForAIDS");

                    b.Property<Guid>("GbvCaseId")
                        .HasColumnName("gbvCaseId");

                    b.Property<int?>("HcvHbs")
                        .HasColumnName("hcv_hbs");

                    b.Property<DateTime?>("IncDate")
                        .HasColumnName("incDate")
                        .HasColumnType("date");

                    b.Property<int?>("IncLocation")
                        .HasColumnName("incLocation");

                    b.Property<string>("IncLocationOther")
                        .HasColumnName("incLocationOther")
                        .HasMaxLength(500);

                    b.Property<int?>("IncReportToOther")
                        .HasColumnName("incReportToOther");

                    b.Property<TimeSpan?>("IncTime")
                        .HasColumnName("incTime");

                    b.Property<int?>("IncType")
                        .HasColumnName("incType");

                    b.Property<DateTime?>("InsertDate")
                        .HasColumnName("insertDate")
                        .HasColumnType("date");

                    b.Property<int?>("IsSurvivorWilling")
                        .HasColumnName("isSurvivorWilling");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnName("lastUpdate")
                        .HasColumnType("date");

                    b.Property<int?>("MedExamProcedure")
                        .HasColumnName("medExamProcedure");

                    b.Property<int?>("MedForInjuries")
                        .HasColumnName("med_for_injuries");

                    b.Property<int?>("MedicalCertificate")
                        .HasColumnName("medicalCertificate");

                    b.Property<int?>("Mowadowa")
                        .HasColumnName("MOWADOWA");

                    b.Property<int?>("NoOfPerpetrators")
                        .HasColumnName("noOfPerpetrators");

                    b.Property<int?>("NoServiceAccepted")
                        .HasColumnName("noServiceAccepted");

                    b.Property<int?>("NoServiceAvailable")
                        .HasColumnName("noServiceAvailable");

                    b.Property<int?>("NoServiceProvided")
                        .HasColumnName("noServiceProvided");

                    b.Property<string>("OccupationOther")
                        .HasColumnName("occupationOther")
                        .HasMaxLength(500);

                    b.Property<int?>("Other")
                        .HasColumnName("other");

                    b.Property<string>("OtherSpecify")
                        .HasColumnName("otherSpecify")
                        .HasMaxLength(300);

                    b.Property<int?>("PerpetratorOccupation")
                        .HasColumnName("perpetratorOccupation");

                    b.Property<int?>("PregnancyWeeks")
                        .HasColumnName("pregnancyWeeks");

                    b.Property<string>("PsychologicalStatus")
                        .HasColumnName("psychologicalStatus")
                        .HasMaxLength(500);

                    b.Property<int?>("RefFpcenter")
                        .HasColumnName("refFPCenter");

                    b.Property<int?>("RefNotAccepted")
                        .HasColumnName("refNotAccepted");

                    b.Property<int?>("RefNotAvailable")
                        .HasColumnName("refNotAvailable");

                    b.Property<string>("RefOther")
                        .HasColumnName("refOther")
                        .HasMaxLength(500);

                    b.Property<int?>("ReferToHigherMedical")
                        .HasColumnName("referToHigherMedical");

                    b.Property<int?>("ReferToPsychosocial")
                        .HasColumnName("referToPsychosocial");

                    b.Property<int>("ReferredBy")
                        .HasColumnName("referredBy");

                    b.Property<int?>("Relationship")
                        .HasColumnName("relationship");

                    b.Property<int?>("Results")
                        .HasColumnName("results");

                    b.Property<string>("ResultsOther")
                        .HasColumnName("resultsOther")
                        .HasMaxLength(500);

                    b.Property<int?>("SafeHouse")
                        .HasColumnName("safeHouse");

                    b.Property<int?>("SpreadInfo")
                        .HasColumnName("spreadInfo");

                    b.Property<int?>("SstPrevenMed")
                        .HasColumnName("SST_preven_med");

                    b.Property<int?>("StiStatus")
                        .HasColumnName("stiStatus");

                    b.Property<string>("UserName")
                        .HasColumnName("userName")
                        .HasMaxLength(50);

                    b.Property<int?>("VacForTreat")
                        .HasColumnName("vac_for_treat");

                    b.Property<int?>("YesBetterFacilities")
                        .HasColumnName("yesBetterFacilities");

                    b.Property<int?>("YesConsulation")
                        .HasColumnName("yesConsulation");

                    b.Property<int?>("YesFpservices")
                        .HasColumnName("yesFPServices");

                    b.Property<int?>("YesOther")
                        .HasColumnName("yesOther");

                    b.Property<int?>("YesPregnancyTest")
                        .HasColumnName("yesPregnancyTest");

                    b.Property<int?>("YesVaccination")
                        .HasColumnName("yesVaccination");

                    b.Property<int?>("YesVct")
                        .HasColumnName("yesVCT");

                    b.HasKey("IntakeInfoId");

                    b.HasIndex("GbvCaseId");

                    b.ToTable("IntakeInfo");
                });

            modelBuilder.Entity("rmc.Models.Provinces", b =>
                {
                    b.Property<string>("ProvCode")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<int?>("Aghchocode")
                        .HasColumnName("AGHCHOCode");

                    b.Property<int?>("Aimscode")
                        .HasColumnName("AIMSCode");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("ProvName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("ProveNameDari")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("ProveNamePashto")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("ProvCode")
                        .HasName("PK_Provinces");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("rmc.Models.Registration", b =>
                {
                    b.Property<int>("RegistrationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("registrationId");

                    b.Property<Guid>("GbvCaseId")
                        .HasColumnName("gbvCaseId");

                    b.Property<int>("GbvHistory")
                        .HasColumnName("gbvHistory");

                    b.Property<DateTime?>("InsertDate")
                        .HasColumnName("insertDate")
                        .HasColumnType("date");

                    b.Property<int>("IsIdp")
                        .HasColumnName("isIDP");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnName("lastUpdate")
                        .HasColumnType("date");

                    b.Property<int?>("RefIn");

                    b.Property<int?>("RefOut");

                    b.Property<string>("Remarks")
                        .HasMaxLength(500);

                    b.Property<int>("RptMonth")
                        .HasColumnName("rptMonth");

                    b.Property<int>("RptYear")
                        .HasColumnName("rptYear");

                    b.Property<int?>("ServiceMedical")
                        .HasColumnName("serviceMedical");

                    b.Property<int?>("ServicePsycho")
                        .HasColumnName("servicePsycho");

                    b.Property<int?>("ServiceRefLegal")
                        .HasColumnName("serviceRefLegal");

                    b.Property<int?>("ServiceRefSafeHouse")
                        .HasColumnName("serviceRefSafeHouse");

                    b.Property<int>("TypeOfViolence")
                        .HasColumnName("typeOfViolence");

                    b.Property<string>("UserName")
                        .HasColumnName("userName")
                        .HasMaxLength(50);

                    b.HasKey("RegistrationId");

                    b.HasIndex("GbvCaseId");

                    b.ToTable("registration");
                });

            modelBuilder.Entity("rmc.Models.Tenant", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id")
                        .HasName("PK_tenant");

                    b.ToTable("Tenant");
                });

            modelBuilder.Entity("rmc.Models.Authorization", b =>
                {
                    b.HasOne("rmc.Models.GbvCase", "GbvCase")
                        .WithMany("Authorization")
                        .HasForeignKey("GbvCaseId")
                        .HasConstraintName("FK_Authorization_gbvCase")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("rmc.Models.AuthorizationSub", b =>
                {
                    b.HasOne("rmc.Models.AgencyType", "AgencyType")
                        .WithMany("AuthorizationSub")
                        .HasForeignKey("AgencyTypeId")
                        .HasConstraintName("FK_Authorization_sub_AgencyType");

                    b.HasOne("rmc.Models.Authorization", "Authorization")
                        .WithMany("AuthorizationSub")
                        .HasForeignKey("AuthorizationId")
                        .HasConstraintName("FK_Authorization_sub_Authorization")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("rmc.Models.Consent", b =>
                {
                    b.HasOne("rmc.Models.GbvCase", "GbvCase")
                        .WithMany("Consent")
                        .HasForeignKey("GbvCaseId")
                        .HasConstraintName("FK_consent_gbvCase")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("rmc.Models.Districts", b =>
                {
                    b.HasOne("rmc.Models.Provinces", "ProvCodeNavigation")
                        .WithMany("Districts")
                        .HasForeignKey("ProvCode");
                });

            modelBuilder.Entity("rmc.Models.FacilityInfo", b =>
                {
                    b.HasOne("rmc.Models.Districts", "DistCodeNavigation")
                        .WithMany("FacilityInfo")
                        .HasForeignKey("DistCode")
                        .HasConstraintName("FK_FacilityInfo_Districts");
                });

            modelBuilder.Entity("rmc.Models.GbvCase", b =>
                {
                    b.HasOne("rmc.Models.FacilityInfo", "Hospital")
                        .WithMany("GbvCase")
                        .HasForeignKey("HospitalId")
                        .HasConstraintName("FK_gbvCase_FacilityInfo");
                });

            modelBuilder.Entity("rmc.Models.IntakeInfo", b =>
                {
                    b.HasOne("rmc.Models.GbvCase", "GbvCase")
                        .WithMany("IntakeInfo")
                        .HasForeignKey("GbvCaseId")
                        .HasConstraintName("FK_IntakeInfo_gbvCase")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("rmc.Models.Registration", b =>
                {
                    b.HasOne("rmc.Models.GbvCase", "GbvCase")
                        .WithMany("Registration")
                        .HasForeignKey("GbvCaseId")
                        .HasConstraintName("FK_registration_gbvCase")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
