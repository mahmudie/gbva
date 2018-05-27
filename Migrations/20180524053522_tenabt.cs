using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace rmc.Migrations
{
    public partial class tenabt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgencyType",
                columns: table => new
                {
                    AgencyTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AgencyType = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyType", x => x.AgencyTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    ProvCode = table.Column<string>(maxLength: 50, nullable: false),
                    AGHCHOCode = table.Column<int>(nullable: true),
                    AIMSCode = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    ProvName = table.Column<string>(maxLength: 255, nullable: false),
                    ProveNameDari = table.Column<string>(maxLength: 50, nullable: false),
                    ProveNamePashto = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.ProvCode);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    DistCode = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    DistName = table.Column<string>(maxLength: 255, nullable: false),
                    DistNameDari = table.Column<string>(maxLength: 50, nullable: true),
                    DistNamePashto = table.Column<string>(maxLength: 255, nullable: true),
                    ProvCode = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.DistCode);
                    table.ForeignKey(
                        name: "FK_Districts_Provinces_ProvCode",
                        column: x => x.ProvCode,
                        principalTable: "Provinces",
                        principalColumn: "ProvCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FacilityInfo",
                columns: table => new
                {
                    FacilityID = table.Column<int>(nullable: false),
                    ActiveStatus = table.Column<string>(maxLength: 10, nullable: true),
                    DateEstablished = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    DistCode = table.Column<string>(maxLength: 50, nullable: false),
                    FacilityName = table.Column<string>(maxLength: 255, nullable: false),
                    FacilityNameDari = table.Column<string>(maxLength: 255, nullable: true),
                    FacilityNamePashto = table.Column<string>(maxLength: 255, nullable: true),
                    FacilityType = table.Column<int>(nullable: false),
                    GPSLattitude = table.Column<double>(nullable: true),
                    GPSLongtitude = table.Column<double>(nullable: true),
                    Implementer = table.Column<string>(nullable: false),
                    LAT = table.Column<double>(nullable: true),
                    Location = table.Column<string>(maxLength: 100, nullable: true),
                    LocationDari = table.Column<string>(maxLength: 100, nullable: true),
                    LocationPashto = table.Column<string>(maxLength: 100, nullable: true),
                    LON = table.Column<double>(nullable: true),
                    SubImplementer = table.Column<string>(maxLength: 255, nullable: true),
                    ViliCode = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityInfo", x => x.FacilityID);
                    table.ForeignKey(
                        name: "FK_FacilityInfo_Districts",
                        column: x => x.DistCode,
                        principalTable: "Districts",
                        principalColumn: "DistCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "gbvCase",
                columns: table => new
                {
                    gbvCaseId = table.Column<Guid>(nullable: false),
                    address = table.Column<string>(maxLength: 255, nullable: true),
                    age = table.Column<int>(nullable: true),
                    hospitalId = table.Column<int>(nullable: true),
                    incCode = table.Column<string>(maxLength: 50, nullable: true),
                    insertDate = table.Column<DateTime>(type: "date", nullable: true),
                    itrCode = table.Column<string>(maxLength: 50, nullable: true),
                    LastUpdate = table.Column<DateTime>(type: "date", nullable: true),
                    maritalStatus = table.Column<int>(nullable: true),
                    patientFatherName = table.Column<string>(maxLength: 255, nullable: true),
                    patientName = table.Column<string>(maxLength: 255, nullable: true),
                    refDate = table.Column<DateTime>(type: "date", nullable: true),
                    refTime = table.Column<TimeSpan>(nullable: true),
                    regNo = table.Column<int>(nullable: true),
                    rptCode = table.Column<string>(maxLength: 50, nullable: true),
                    sex = table.Column<int>(nullable: true),
                    userName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gbvCase", x => x.gbvCaseId);
                    table.ForeignKey(
                        name: "FK_gbvCase_FacilityInfo",
                        column: x => x.hospitalId,
                        principalTable: "FacilityInfo",
                        principalColumn: "FacilityID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Authorization",
                columns: table => new
                {
                    AuthorizationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    gbvCaseId = table.Column<Guid>(nullable: true),
                    insertDate = table.Column<DateTime>(type: "date", nullable: true),
                    isSigned = table.Column<bool>(nullable: true),
                    islessthan18 = table.Column<bool>(nullable: true),
                    lastUpdate = table.Column<DateTime>(type: "date", nullable: true),
                    userName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authorization", x => x.AuthorizationId);
                    table.ForeignKey(
                        name: "FK_Authorization_gbvCase",
                        column: x => x.gbvCaseId,
                        principalTable: "gbvCase",
                        principalColumn: "gbvCaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "consent",
                columns: table => new
                {
                    consentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    bloodExams = table.Column<int>(nullable: false),
                    gbvCaseId = table.Column<Guid>(nullable: false),
                    insertDate = table.Column<DateTime>(type: "date", nullable: true),
                    lastUpdate = table.Column<DateTime>(type: "date", nullable: true),
                    otherExams = table.Column<int>(nullable: false),
                    pelvicExams = table.Column<int>(nullable: false),
                    physicalExams = table.Column<int>(nullable: false),
                    provisionOfInfo = table.Column<int>(nullable: false),
                    speculumExams = table.Column<int>(nullable: false),
                    userName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_consent", x => x.consentId);
                    table.ForeignKey(
                        name: "FK_consent_gbvCase",
                        column: x => x.gbvCaseId,
                        principalTable: "gbvCase",
                        principalColumn: "gbvCaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IntakeInfo",
                columns: table => new
                {
                    intakeInfoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ageGroup = table.Column<int>(nullable: true),
                    AIHRC = table.Column<int>(nullable: true),
                    appointment = table.Column<int>(nullable: true),
                    backHome = table.Column<int>(nullable: true),
                    consentTaken = table.Column<int>(nullable: true),
                    evidenceCollected = table.Column<int>(nullable: true),
                    evidenceOfPregnancy = table.Column<int>(nullable: true),
                    examDate = table.Column<DateTime>(type: "date", nullable: true),
                    examTime = table.Column<TimeSpan>(nullable: true),
                    examinedForAIDS = table.Column<int>(nullable: true),
                    gbvCaseId = table.Column<Guid>(nullable: false),
                    hcv_hbs = table.Column<int>(nullable: true),
                    incDate = table.Column<DateTime>(type: "date", nullable: true),
                    incLocation = table.Column<int>(nullable: true),
                    incLocationOther = table.Column<string>(maxLength: 500, nullable: true),
                    incReportToOther = table.Column<int>(nullable: true),
                    incTime = table.Column<TimeSpan>(nullable: true),
                    incType = table.Column<int>(nullable: true),
                    insertDate = table.Column<DateTime>(type: "date", nullable: true),
                    isSurvivorWilling = table.Column<int>(nullable: true),
                    lastUpdate = table.Column<DateTime>(type: "date", nullable: true),
                    medExamProcedure = table.Column<int>(nullable: true),
                    med_for_injuries = table.Column<int>(nullable: true),
                    medicalCertificate = table.Column<int>(nullable: true),
                    MOWADOWA = table.Column<int>(nullable: true),
                    noOfPerpetrators = table.Column<int>(nullable: true),
                    noServiceAccepted = table.Column<int>(nullable: true),
                    noServiceAvailable = table.Column<int>(nullable: true),
                    noServiceProvided = table.Column<int>(nullable: true),
                    occupationOther = table.Column<string>(maxLength: 500, nullable: true),
                    other = table.Column<int>(nullable: true),
                    otherSpecify = table.Column<string>(maxLength: 300, nullable: true),
                    perpetratorOccupation = table.Column<int>(nullable: true),
                    pregnancyWeeks = table.Column<int>(nullable: true),
                    psychologicalStatus = table.Column<string>(maxLength: 500, nullable: true),
                    refFPCenter = table.Column<int>(nullable: true),
                    refNotAccepted = table.Column<int>(nullable: true),
                    refNotAvailable = table.Column<int>(nullable: true),
                    refOther = table.Column<string>(maxLength: 500, nullable: true),
                    referToHigherMedical = table.Column<int>(nullable: true),
                    referToPsychosocial = table.Column<int>(nullable: true),
                    referredBy = table.Column<int>(nullable: false),
                    relationship = table.Column<int>(nullable: true),
                    results = table.Column<int>(nullable: true),
                    resultsOther = table.Column<string>(maxLength: 500, nullable: true),
                    safeHouse = table.Column<int>(nullable: true),
                    spreadInfo = table.Column<int>(nullable: true),
                    SST_preven_med = table.Column<int>(nullable: true),
                    stiStatus = table.Column<int>(nullable: true),
                    userName = table.Column<string>(maxLength: 50, nullable: true),
                    vac_for_treat = table.Column<int>(nullable: true),
                    yesBetterFacilities = table.Column<int>(nullable: true),
                    yesConsulation = table.Column<int>(nullable: true),
                    yesFPServices = table.Column<int>(nullable: true),
                    yesOther = table.Column<int>(nullable: true),
                    yesPregnancyTest = table.Column<int>(nullable: true),
                    yesVaccination = table.Column<int>(nullable: true),
                    yesVCT = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntakeInfo", x => x.intakeInfoId);
                    table.ForeignKey(
                        name: "FK_IntakeInfo_gbvCase",
                        column: x => x.gbvCaseId,
                        principalTable: "gbvCase",
                        principalColumn: "gbvCaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "registration",
                columns: table => new
                {
                    registrationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    gbvCaseId = table.Column<Guid>(nullable: false),
                    gbvHistory = table.Column<int>(nullable: false),
                    insertDate = table.Column<DateTime>(type: "date", nullable: true),
                    isIDP = table.Column<int>(nullable: false),
                    lastUpdate = table.Column<DateTime>(type: "date", nullable: true),
                    RefIn = table.Column<int>(nullable: true),
                    RefOut = table.Column<int>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 500, nullable: true),
                    rptMonth = table.Column<int>(nullable: false),
                    rptYear = table.Column<int>(nullable: false),
                    serviceMedical = table.Column<int>(nullable: true),
                    servicePsycho = table.Column<int>(nullable: true),
                    serviceRefLegal = table.Column<int>(nullable: true),
                    serviceRefSafeHouse = table.Column<int>(nullable: true),
                    typeOfViolence = table.Column<int>(nullable: false),
                    userName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registration", x => x.registrationId);
                    table.ForeignKey(
                        name: "FK_registration_gbvCase",
                        column: x => x.gbvCaseId,
                        principalTable: "gbvCase",
                        principalColumn: "gbvCaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Authorization_sub",
                columns: table => new
                {
                    Authorization_subId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AgencyName = table.Column<string>(maxLength: 100, nullable: true),
                    AgencyTypeId = table.Column<int>(nullable: true),
                    AuthorizationId = table.Column<int>(nullable: true),
                    Comment = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authorization_sub", x => x.Authorization_subId);
                    table.ForeignKey(
                        name: "FK_Authorization_sub_AgencyType",
                        column: x => x.AgencyTypeId,
                        principalTable: "AgencyType",
                        principalColumn: "AgencyTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Authorization_sub_Authorization",
                        column: x => x.AuthorizationId,
                        principalTable: "Authorization",
                        principalColumn: "AuthorizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authorization_gbvCaseId",
                table: "Authorization",
                column: "gbvCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Authorization_sub_AgencyTypeId",
                table: "Authorization_sub",
                column: "AgencyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Authorization_sub_AuthorizationId",
                table: "Authorization_sub",
                column: "AuthorizationId");

            migrationBuilder.CreateIndex(
                name: "IX_consent_gbvCaseId",
                table: "consent",
                column: "gbvCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_ProvCode",
                table: "Districts",
                column: "ProvCode");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityInfo_DistCode",
                table: "FacilityInfo",
                column: "DistCode");

            migrationBuilder.CreateIndex(
                name: "IX_gbvCase_hospitalId",
                table: "gbvCase",
                column: "hospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_IntakeInfo_gbvCaseId",
                table: "IntakeInfo",
                column: "gbvCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_registration_gbvCaseId",
                table: "registration",
                column: "gbvCaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authorization_sub");

            migrationBuilder.DropTable(
                name: "consent");

            migrationBuilder.DropTable(
                name: "IntakeInfo");

            migrationBuilder.DropTable(
                name: "registration");

            migrationBuilder.DropTable(
                name: "AgencyType");

            migrationBuilder.DropTable(
                name: "Authorization");

            migrationBuilder.DropTable(
                name: "gbvCase");

            migrationBuilder.DropTable(
                name: "FacilityInfo");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Provinces");
        }
    }
}
