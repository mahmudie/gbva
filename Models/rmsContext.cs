using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace rmc.Models
{
    public partial class rmsContext : DbContext
    {
        public rmsContext(DbContextOptions<rmsContext> options)
            : base(options)
        {
        }
        public virtual DbSet<AgencyType> AgencyType { get; set; }
        public virtual DbSet<Authorization> Authorization { get; set; }
        public virtual DbSet<AuthorizationSub> AuthorizationSub { get; set; }
        public virtual DbSet<Consent> Consent { get; set; }
        public virtual DbSet<Districts> Districts { get; set; }
        public virtual DbSet<FacilityInfo> FacilityInfo { get; set; }
        public virtual DbSet<GbvCase> GbvCase { get; set; }
        public virtual DbSet<IntakeInfo> IntakeInfo { get; set; }
        public virtual DbSet<Provinces> Provinces { get; set; }
        public virtual DbSet<Registration> Registration { get; set; }
        public virtual DbSet<Tenant> Tenants { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgencyType>(entity =>
            {
                entity.Property(e => e.AgencyType1)
                    .HasColumnName("AgencyType")
                    .HasMaxLength(100);
            });
            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.ToTable("Tenant");

                entity.HasKey(e => e.Id)
                    .HasName("PK_tenant");
                entity.Property(e => e.Name).HasMaxLength(255);


            });
            modelBuilder.Entity<Authorization>(entity =>
            {
                entity.Property(e => e.GbvCaseId).HasColumnName("gbvCaseId");
                entity.Property(e => e.IsSigned).HasColumnName("isSigned");
                entity.Property(e => e.Islessthan18).HasColumnName("islessthan18");

                entity.HasOne(d => d.GbvCase)
                    .WithMany(p => p.Authorization)
                    .HasForeignKey(d => d.GbvCaseId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Authorization_gbvCase");
            });

            modelBuilder.Entity<AuthorizationSub>(entity =>
            {
                entity.ToTable("Authorization_sub");

                entity.Property(e => e.AuthorizationSubId).HasColumnName("Authorization_subId");

                entity.Property(e => e.AgencyName).HasMaxLength(100);

                entity.Property(e => e.Comment).HasMaxLength(250);

                entity.HasOne(d => d.AgencyType)
                    .WithMany(p => p.AuthorizationSub)
                    .HasForeignKey(d => d.AgencyTypeId)
                    .HasConstraintName("FK_Authorization_sub_AgencyType");

                entity.HasOne(d => d.Authorization)
                    .WithMany(p => p.AuthorizationSub)
                    .HasForeignKey(d => d.AuthorizationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Authorization_sub_Authorization");
            });

            modelBuilder.Entity<Consent>(entity =>
            {
                entity.ToTable("consent");

                entity.Property(e => e.ConsentId).HasColumnName("consentId");

                entity.Property(e => e.BloodExams).HasColumnName("bloodExams");

                entity.Property(e => e.GbvCaseId).HasColumnName("gbvCaseId");

                entity.Property(e => e.InsertDate)
                    .HasColumnName("insertDate")
                    .HasColumnType("date");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("lastUpdate")
                    .HasColumnType("date");

                entity.Property(e => e.OtherExams).HasColumnName("otherExams");

                entity.Property(e => e.PelvicExams).HasColumnName("pelvicExams");

                entity.Property(e => e.PhysicalExams).HasColumnName("physicalExams");

                entity.Property(e => e.ProvisionOfInfo).HasColumnName("provisionOfInfo");

                entity.Property(e => e.SpeculumExams).HasColumnName("speculumExams");

                entity.Property(e => e.UserName)
                    .HasColumnName("userName")
                    .HasMaxLength(50);

                entity.HasOne(d => d.GbvCase)
                    .WithMany(p => p.Consent)
                    .HasForeignKey(d => d.GbvCaseId)
                    .HasConstraintName("FK_consent_gbvCase");
            });

            modelBuilder.Entity<Districts>(entity =>
            {
                entity.HasKey(e => e.DistCode)
                    .HasName("PK_Districts");

                entity.Property(e => e.DistCode).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.DistName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.DistNameDari).HasMaxLength(50);

                entity.Property(e => e.DistNamePashto).HasMaxLength(255);

                entity.Property(e => e.ProvCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ProvCodeNavigation)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.ProvCode)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Districts_Provinces");
            });

            modelBuilder.Entity<FacilityInfo>(entity =>
            {
                entity.HasKey(e => e.FacilityId)
                    .HasName("PK_FacilityInfo");

                entity.Property(e => e.FacilityId)
                    .HasColumnName("FacilityID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ActiveStatus).HasMaxLength(10);

                entity.Property(e => e.DateEstablished).HasColumnType("datetime2(0)");

                entity.Property(e => e.DistCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FacilityName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FacilityNameDari).HasMaxLength(255);

                entity.Property(e => e.FacilityNamePashto).HasMaxLength(255);

                entity.Property(e => e.Gpslattitude).HasColumnName("GPSLattitude");

                entity.Property(e => e.Gpslongtitude).HasColumnName("GPSLongtitude");

                entity.Property(e => e.Implementer).IsRequired();

                entity.Property(e => e.Lat).HasColumnName("LAT");

                entity.Property(e => e.Location).HasMaxLength(100);

                entity.Property(e => e.LocationDari).HasMaxLength(100);

                entity.Property(e => e.LocationPashto).HasMaxLength(100);

                entity.Property(e => e.Lon).HasColumnName("LON");

                entity.Property(e => e.SubImplementer).HasMaxLength(255);

                entity.Property(e => e.ViliCode).HasMaxLength(255);

                entity.HasOne(d => d.DistCodeNavigation)
                    .WithMany(p => p.FacilityInfo)
                    .HasForeignKey(d => d.DistCode)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_FacilityInfo_Districts");
            });

            modelBuilder.Entity<GbvCase>(entity =>
            {
                entity.ToTable("gbvCase");

                entity.Property(e => e.GbvCaseId)
                    .HasColumnName("gbvCaseId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(255);

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.HospitalId).HasColumnName("hospitalId");

                entity.Property(e => e.IncCode)
                    .HasColumnName("incCode")
                    .HasMaxLength(50);

                entity.Property(e => e.InsertDate)
                    .HasColumnName("insertDate")
                    .HasColumnType("date");

                entity.Property(e => e.ItrCode)
                    .HasColumnName("itrCode")
                    .HasMaxLength(50);

                entity.Property(e => e.LastUpdate).HasColumnType("date");

                entity.Property(e => e.MaritalStatus).HasColumnName("maritalStatus");

                entity.Property(e => e.PatientFatherName)
                    .HasColumnName("patientFatherName")
                    .HasMaxLength(255);

                entity.Property(e => e.PatientName)
                    .HasColumnName("patientName")
                    .HasMaxLength(255);

                entity.Property(e => e.RefDate)
                    .HasColumnName("refDate")
                    .HasColumnType("date");

                entity.Property(e => e.RefTime).HasColumnName("refTime");

                entity.Property(e => e.RegNo).HasColumnName("regNo");
                entity.Property(e => e.Sex).HasColumnName("sex");

                entity.Property(e => e.UserName)
                    .HasColumnName("userName")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Hospital)
                    .WithMany(p => p.GbvCase)
                    .HasForeignKey(d => d.HospitalId)
                    .HasConstraintName("FK_gbvCase_FacilityInfo");
            });

            modelBuilder.Entity<IntakeInfo>(entity =>
            {
                entity.Property(e => e.IntakeInfoId).HasColumnName("intakeInfoId");

                entity.Property(e => e.AgeGroup).HasColumnName("ageGroup");

                entity.Property(e => e.Aihrc).HasColumnName("AIHRC");

                entity.Property(e => e.Appointment).HasColumnName("appointment");

                entity.Property(e => e.BackHome).HasColumnName("backHome");

                entity.Property(e => e.ConsentTaken).HasColumnName("consentTaken");

                entity.Property(e => e.EvidenceCollected).HasColumnName("evidenceCollected");

                entity.Property(e => e.EvidenceOfPregnancy).HasColumnName("evidenceOfPregnancy");

                entity.Property(e => e.ExamDate)
                    .HasColumnName("examDate")
                    .HasColumnType("date");

                entity.Property(e => e.ExamTime).HasColumnName("examTime");

                entity.Property(e => e.ExaminedForAids).HasColumnName("examinedForAIDS");

                entity.Property(e => e.GbvCaseId).HasColumnName("gbvCaseId");

                entity.Property(e => e.HcvHbs).HasColumnName("hcv_hbs");

                entity.Property(e => e.IncDate)
                    .HasColumnName("incDate")
                    .HasColumnType("date");

                entity.Property(e => e.IncLocation).HasColumnName("incLocation");

                entity.Property(e => e.IncLocationOther)
                    .HasColumnName("incLocationOther")
                    .HasMaxLength(500);

                entity.Property(e => e.IncReportToOther).HasColumnName("incReportToOther");

                entity.Property(e => e.IncTime).HasColumnName("incTime");

                entity.Property(e => e.IncType).HasColumnName("incType");

                entity.Property(e => e.InsertDate)
                    .HasColumnName("insertDate")
                    .HasColumnType("date");

                entity.Property(e => e.IsSurvivorWilling).HasColumnName("isSurvivorWilling");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("lastUpdate")
                    .HasColumnType("date");

                entity.Property(e => e.MedExamProcedure).HasColumnName("medExamProcedure");

                entity.Property(e => e.MedForInjuries).HasColumnName("med_for_injuries");

                entity.Property(e => e.MedicalCertificate).HasColumnName("medicalCertificate");

                entity.Property(e => e.Mowadowa).HasColumnName("MOWADOWA");

                entity.Property(e => e.NoOfPerpetrators).HasColumnName("noOfPerpetrators");

                entity.Property(e => e.NoServiceAccepted).HasColumnName("noServiceAccepted");

                entity.Property(e => e.NoServiceAvailable).HasColumnName("noServiceAvailable");

                entity.Property(e => e.NoServiceProvided).HasColumnName("noServiceProvided");

                entity.Property(e => e.OccupationOther)
                    .HasColumnName("occupationOther")
                    .HasMaxLength(500);

                entity.Property(e => e.Other).HasColumnName("other");

                entity.Property(e => e.OtherSpecify)
                    .HasColumnName("otherSpecify")
                    .HasMaxLength(300);

                entity.Property(e => e.PerpetratorOccupation).HasColumnName("perpetratorOccupation");

                entity.Property(e => e.PregnancyWeeks).HasColumnName("pregnancyWeeks");

                entity.Property(e => e.PsychologicalStatus)
                    .HasColumnName("psychologicalStatus")
                    .HasMaxLength(500);

                entity.Property(e => e.RefFpcenter).HasColumnName("refFPCenter");

                entity.Property(e => e.RefNotAccepted).HasColumnName("refNotAccepted");

                entity.Property(e => e.RefNotAvailable).HasColumnName("refNotAvailable");

                entity.Property(e => e.RefOther)
                    .HasColumnName("refOther")
                    .HasMaxLength(500);

                entity.Property(e => e.ReferToHigherMedical).HasColumnName("referToHigherMedical");

                entity.Property(e => e.ReferToPsychosocial).HasColumnName("referToPsychosocial");

                entity.Property(e => e.ReferredBy).HasColumnName("referredBy");

                entity.Property(e => e.Relationship).HasColumnName("relationship");

                entity.Property(e => e.Results).HasColumnName("results");

                entity.Property(e => e.ResultsOther)
                    .HasColumnName("resultsOther")
                    .HasMaxLength(500);

                entity.Property(e => e.SafeHouse).HasColumnName("safeHouse");

                entity.Property(e => e.SpreadInfo).HasColumnName("spreadInfo");

                entity.Property(e => e.SstPrevenMed).HasColumnName("SST_preven_med");

                entity.Property(e => e.StiStatus).HasColumnName("stiStatus");

                entity.Property(e => e.UserName)
                    .HasColumnName("userName")
                    .HasMaxLength(50);

                entity.Property(e => e.VacForTreat).HasColumnName("vac_for_treat");

                entity.Property(e => e.YesBetterFacilities).HasColumnName("yesBetterFacilities");

                entity.Property(e => e.YesConsulation).HasColumnName("yesConsulation");

                entity.Property(e => e.YesFpservices).HasColumnName("yesFPServices");

                entity.Property(e => e.YesOther).HasColumnName("yesOther");

                entity.Property(e => e.YesPregnancyTest).HasColumnName("yesPregnancyTest");

                entity.Property(e => e.YesVaccination).HasColumnName("yesVaccination");

                entity.Property(e => e.YesVct).HasColumnName("yesVCT");

                entity.HasOne(d => d.GbvCase)
                    .WithMany(p => p.IntakeInfo)
                    .HasForeignKey(d => d.GbvCaseId)
                    .HasConstraintName("FK_IntakeInfo_gbvCase");
            });

            modelBuilder.Entity<Provinces>(entity =>
            {
                entity.HasKey(e => e.ProvCode)
                    .HasName("PK_Provinces");

                entity.Property(e => e.ProvCode).HasMaxLength(50);

                entity.Property(e => e.Aghchocode).HasColumnName("AGHCHOCode");

                entity.Property(e => e.Aimscode).HasColumnName("AIMSCode");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.ProvName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ProveNameDari)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProveNamePashto)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.ToTable("registration");

                entity.Property(e => e.RegistrationId).HasColumnName("registrationId");

                entity.Property(e => e.GbvCaseId).HasColumnName("gbvCaseId");

                entity.Property(e => e.GbvHistory).HasColumnName("gbvHistory");

                entity.Property(e => e.InsertDate)
                    .HasColumnName("insertDate")
                    .HasColumnType("date");

                entity.Property(e => e.IsIdp).HasColumnName("isIDP");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("lastUpdate")
                    .HasColumnType("date");

                entity.Property(e => e.Remarks).HasMaxLength(500);

                entity.Property(e => e.RptMonth).HasColumnName("rptMonth");

                entity.Property(e => e.RptYear).HasColumnName("rptYear");

                entity.Property(e => e.ServiceMedical).HasColumnName("serviceMedical");

                entity.Property(e => e.ServicePsycho).HasColumnName("servicePsycho");

                entity.Property(e => e.ServiceRefLegal).HasColumnName("serviceRefLegal");

                entity.Property(e => e.ServiceRefSafeHouse).HasColumnName("serviceRefSafeHouse");

                entity.Property(e => e.TypeOfViolence).HasColumnName("typeOfViolence");

                entity.Property(e => e.UserName)
                    .HasColumnName("userName")
                    .HasMaxLength(50);

                entity.HasOne(d => d.GbvCase)
                    .WithMany(p => p.Registration)
                    .HasForeignKey(d => d.GbvCaseId)
                    .HasConstraintName("FK_registration_gbvCase");
            });
        }
    }
}