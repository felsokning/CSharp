//-----------------------------------------------------------------------
// <copyright file="TypeEnums.cs" company="None">
//     Copyright (c) felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Public.Exchange.Research.Objects
{
    using System;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TypeEnums"/> class.
    /// </summary>
    public class TypeEnums
    {
        /// NOTE:
        ///     These enums are bitwise flags and, as such, can be stored as multi-valued via sums.
        ///     For more information on this, please see:
        ///     https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/enumeration-types#enumeration-types-as-bit-flags


        /// <summary>
        ///     The possible values for the <see cref="MsExchRecipientDisplayType"/> property.
        /// </summary>
        [Flags]
        public enum MsExchRecipientDisplayType
        {
            MailboxUser = 0,
            DistributionGroup = 1,
            PublicFolder = 2,
            DynamicDistributionGroup = 3,
            Organization = 4,
            PrivateDistributionList = 5,
            RemoteMailUser = 6,
            ConferenceRoomMailbox = 7,
            EquipmentMailbox = 8,
            ArbitrationMailbox = 10,
            MailboxPlan = 11,
            LinkedUser = 12,
            RoomList = 15,
            SecurityDistributionGroup = 1073741833,
            ACLableMailboxUser = 1073741824,
            ACLableRemoteMailUser = 1073741830,
            SyncedUSGasUDG = -2147481343,
            SyncedUSGasUSG = -1073739511,
            SyncedUSGasContact = -2147481338,
            ACLableSyncedUSGasContact = -1073739514,
            SyncedDynamicDistributionGroup = -2147482874,
            ACLableSyncedMailboxUser = -1073741818,
            SyncedMailboxUser = -2147483642,
            SyncedConferenceRoomMailbox = -2147481850,
            SyncedEquipmentMailbox = -2147481594,
            SyncedRemoteMailUser = -2147482106,
            ACLableSyncedRemoteMailUser = -1073740282,
            SyncedPublicFolder = -2147483130
        }

        /// <summary>
        ///     The possible values for the <see cref="MsExchangeRecipientTypeDetails"/> property.
        /// </summary>
        [Flags]
        public enum MsExchangeRecipientTypeDetails
        {
            None = 0,
            UserMailbox = 1,
            LinkedMailbox = 2,
            SharedMailbox = 4,
            LegacyMailbox = 8,
            RoomMailbox = 16,
            EquipmentMailbox = 32,
            MailContact = 64,
            MailUser = 128,
            MailUniversalDistributionGroup = 256,
            MailNonUniversalGroup = 512,
            MailUniversalSecurityGroup = 1024,
            DynamicDistributionGroup = 2048,
            PublicFolder = 4096,
            SystemAttendantMailbox = 8192,
            SystemMailbox = 16384,
            MailForestContact = 32768,
            User = 65536,
            Contact = 131072,
            UniversalDistributionGroup = 262144,
            UniversalSecurityGroup = 524288,
            NonUniversalGroup = 1048576,
            DisabledUser = 2097152,
            MicrosoftExchange = 4194304,
            ArbitrationMailbox = 8388608,
            MailboxPlan = 16777216,
            LinkedUser = 33554432,
            RoomList = 268435456,
            DiscoveryMailbox = 536870912,
            RoleGroup = 1073741824,
            RemoteUserMailbox = unchecked((int)2147483648),
            Computer = unchecked((int)4294967296),
            RemoteRoomMailbox = unchecked((int)8589934592),
            RemoteEquipmentMailbox = unchecked((int)17179869184),
            RemoteSharedMailbox = unchecked((int)34359738368),
            PublicFolderMailbox = unchecked((int)68719476736),
            TeamMailbox = unchecked((int)137438953472),
            RemoteTeamMailbox = unchecked((int)274877906944),
            MonitoringMailbox = unchecked((int)549755813888),
            GroupMailbox = unchecked((int)1099511627776),
            LinkedRoomMailbox = unchecked((int)2199023255552),
            AuditLogMailbox = unchecked((int)4398046511104),
            RemoteGroupMailbox = unchecked((int)8796093022208),
            SchedulingMailbox = unchecked((int)17592186044416),
            GuestMailUser = unchecked((int)35184372088832),
            AuxAuditLogMailbox = unchecked((int)70368744177664),
            SupervisoryReviewPolicyMailbox = unchecked((int)140737488355328),
        }

        /// <summary>
        ///     The possible values for the <see cref="MsExchRemoteRecipientType"/> property.
        /// </summary>
        [Flags]
        public enum MsExchRemoteRecipientType
        {
            ProvisionedMailbox = 1,
            ProvisionedArchive = 2,
            Migrated = 4,
            DeprovisionMailbox = 8,
            DeprovisionArchive = 16,
            RoomMailbox = 32,
            EquipmentMailbox = 64,
            SharedMailbox = 96
        }
    }
}