import {
	faArchive,
	faBars,
	faCertificate,
	faCog, faCogs,
	faDatabase,
	faEnvelope,
	faFileAlt,
	faFolder,
	faHistory,
	faHome,
	faKey,
	faList,
	faListUl,
	faLock,
	faMailBulk,
	faShareAlt,
	faTasks,
	faUniversity,
	faUnlock,
	faUser,
	faUsers,
	faWindowMaximize
} from '@fortawesome/free-solid-svg-icons';
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import messages from './messages';

@Component({
	name: "the-sidebar",
	i18n: {
		messages: messages
	},
	components: {
	}
})
export default class TheSidebar extends Vue {
	private get isLoggedIn(): boolean {
		return this.$store.getters.isLoggedIn;
	}

	private sidebarItems: SidebarItem[] = [{
		id: "home",
		parentId: null,
		text: "Home",
		url: "/",
		icon: faHome.iconName,
		children: null,
		isOpen: false,
		isSelected: false
	}, {
		id: "uiTest",
		parentId: null,
		text: "UI test",
		url: "/uiTest",
		icon: faUniversity.iconName,
		children: null,
		isOpen: false,
		isSelected: false
	}, {
		id: "procurements",
		parentId: null,
		text: "Procurements",
		url: "#",
		icon: faUniversity.iconName,
		children: [{
			id: "procurementsProcurements",
			parentId: "procurements",
			text: "Procurements",
			url: "#",
			icon: faUniversity.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "procurementsProcurementProcedures",
			parentId: "procurements",
			text: "Procurement procedures",
			url: "#",
			icon: faUniversity.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "procurementsProcurementObjects",
			parentId: "procurements",
			text: "Procurement objects",
			url: "#",
			icon: faUniversity.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}],
		isOpen: false,
		isSelected: false
	}, {
		id: "security",
		parentId: null,
		text: "Security",
		url: "#",
		icon: faLock.iconName,
		children: [{
			id: "securityCommon",
			parentId: "security",
			text: "Common",
			url: "#",
			icon: faLock.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "securityActiveDirectory",
			parentId: "security",
			text: "Active directory",
			url: "#",
			icon: faWindowMaximize.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "securityUsers",
			parentId: "security",
			text: "Users",
			url: "#",
			icon: faUser.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "securityRoles",
			parentId: "security",
			text: "Roles",
			url: "#",
			icon: faArchive.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "securityGroups",
			parentId: "security",
			text: "Groups",
			url: "#",
			icon: faUsers.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "securityAccessControlList",
			parentId: "security",
			text: "Access control list",
			url: "#",
			icon: faUnlock.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		},
		{
			id: "securityCertificates",
			parentId: "security",
			text: "Certificates",
			url: "#",
			icon: faCertificate.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "securitySessions",
			parentId: "security",
			text: "Sessions",
			url: "#",
			icon: faKey.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "securityOAuth",
			parentId: "security",
			text: "OAuth",
			url: "#",
			icon: faUniversity.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}],
		isOpen: false,
		isSelected: false
	}, {
		id: "entities",
		parentId: null,
		text: "Entities",
		url: "#",
		icon: faUser.iconName,
		children: [{
			id: "entitiesPErsons",
			parentId: "entities",
			text: "Persons",
			url: "/entities/persons",
			icon: faUser.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "entitiesOrganisationTypes",
			parentId: "entities",
			text: "Organisation types",
			url: "/entities/organisation-types",
			icon: faUniversity.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "entitiesORganisations",
			parentId: "entities",
			text: "Organisations",
			url: "#",
			icon: faShareAlt.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "entitiesCorrespondents",
			parentId: "entities",
			text: "Correspondents",
			url: "#",
			icon: faUsers.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "entitiesRelationTypes",
			parentId: "entities",
			text: "Relation types",
			url: "#",
			icon: faShareAlt.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "entitiesOrganisationRoles",
			parentId: "entities",
			text: "ORganisation roles",
			url: "#",
			icon: faArchive.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		},
		{
			id: "entitiesPositions",
			parentId: "entities",
			text: "Positions",
			url: "#",
			icon: faUniversity.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}],
		isOpen: false,
		isSelected: false
	}, {
		id: "eMessaging",
		parentId: null,
		text: "e-Messaging",
		url: "#",
		icon: faEnvelope.iconName,
		children: [{
			id: "eMessagingSettings",
			parentId: "eMessaging",
			text: "Settings",
			url: "/emessaging/settings",
			icon: faCogs.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "eMessagingRegistries",
			parentId: "eMessaging",
			text: "Registries",
			url: "#",
			icon: faUniversity.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}],
		isOpen: false,
		isSelected: false
	}, {
		id: "eServices",
		parentId: null,
		text: "eServices",
		url: "#",
		icon: faCogs.iconName,
		children: null,
		isOpen: false,
		isSelected: false
	}, {
		id: "lists",
		parentId: null,
		text: "Lists",
		url: "#",
		icon: faListUl.iconName,
		children: [{
			id: "listsSimpleLists",
			parentId: "lists",
			text: "Simple lists",
			url: "#",
			icon: faFileAlt.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "listsDatabaseLists",
			parentId: "lists",
			text: "Database lists",
			url: "#",
			icon: faDatabase.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "listsPropertySetLists",
			parentId: "lists",
			text: "Property set lists",
			url: "#",
			icon: faBars.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}],
		isOpen: false,
		isSelected: false
	}, {
		id: "dataSources",
		parentId: null,
		text: "Data sources",
		url: "#",
		icon: faList.iconName,
		children: null,
		isOpen: false,
		isSelected: false
	}, {
		id: "eventLog",
		parentId: null,
		text: "Event log",
		url: "#",
		icon: faHistory.iconName,
		children: [{
			id: "eventLogLogFilters",
			parentId: "eventLog",
			text: "Log filters",
			url: "#",
			icon: faBars.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "eventLogSettings",
			parentId: "eventLog",
			text: "Settings",
			url: "#",
			icon: faCogs.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}],
		isOpen: false,
		isSelected: false
	}, {
		id: "propertySetTypes",
		parentId: null,
		text: "Property set types",
		url: "#",
		icon: faUniversity.iconName,
		children: null,
		isOpen: false,
		isSelected: false
	}, {
		id: "attributeTypes",
		parentId: null,
		text: "Attribute types",
		url: "#",
		icon: faTasks.iconName,
		children: null,
		isOpen: false,
		isSelected: false
	}, {
		id: "eDocs",
		parentId: null,
		text: "eDocs",
		url: "#",
		icon: faUniversity.iconName,
		children: [{
			id: "eDocsEDocsSettings",
			parentId: "eDocs",
			text: "eDocs settings",
			url: "#",
			icon: faCogs.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "eDocsDocumentTypes",
			parentId: "eDocs",
			text: "Document types",
			url: "/eDocs/eDocs-types",
			icon: faBars.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "eDocsFolders",
			parentId: "eDocs",
			text: "Folders",
			url: "#",
			icon: faFolder.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "eDocsSampleTexts",
			parentId: "eDocs",
			text: "Sample texts",
			url: "#",
			icon: faFileAlt.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}],
		isOpen: false,
		isSelected: false
	}, {
		id: "eProcess",
		parentId: null,
		text: "eProcess",
		url: "#",
		icon: faUniversity.iconName,
		children: [{
			id: "eProcessSettings",
			parentId: "eProcess",
			text: "Settings",
			url: "#",
			icon: faCogs.iconName,
			children: [{
				id: "eProcessSettingsGeneralSettings",
				parentId: "eProcessSettings",
				text: "General settings",
				url: "#",
				icon: faCog.iconName,
				children: null,
				isOpen: false,
				isSelected: false
			}, {
				id: "eProcessSettingsTaskSettings",
				parentId: "eProcessSettings",
				text: "Task settings",
				url: "#",
				icon: faTasks.iconName,
				children: null,
				isOpen: false,
				isSelected: false
			}, {
				id: "eProcessSettingsAdditionalSettings",
				parentId: "eProcessSettings",
				text: "Additional settings",
				url: "#",
				icon: faCogs.iconName,
				children: null,
				isOpen: false,
				isSelected: false
			}, {
				id: "eProcessSettingsEmailActions",
				parentId: "eProcessSettings",
				text: "Email actions",
				url: "#",
				icon: faMailBulk.iconName,
				children: null,
				isOpen: false,
				isSelected: false
			}],
			isOpen: false,
			isSelected: false
		}, {
			id: "eProcessFolders",
			parentId: "eProcess",
			text: "Folders",
			url: "#",
			icon: faFolder.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "eProcessActivities",
			parentId: "eProcess",
			text: "Activities",
			url: "#",
			icon: faUniversity.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "eProcessRecords",
			parentId: "eProcess",
			text: "Records",
			url: "#",
			icon: faUniversity.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		},
		{
			id: "eProcessDocumentTypes",
			parentId: "eProcess",
			text: "Document types",
			url: "#",
			icon: faUniversity.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "eProcessTexts",
			parentId: "eProcess",
			text: "Texts",
			url: "#",
			icon: faUniversity.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "eProcessProfiles",
			parentId: "eProcess",
			text: "Profiles",
			url: "#",
			icon: faUniversity.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "eProcessPools",
			parentId: "eProcess",
			text: "Pools",
			url: "#",
			icon: faUniversity.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		},
		{
			id: "eProcessModels",
			parentId: "eProcess",
			text: "Models",
			url: "#",
			icon: faUniversity.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "eProcessCorrespondents",
			parentId: "eProcess",
			text: "Correspondents",
			url: "#",
			icon: faUniversity.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "eProcessOrganisation",
			parentId: "eProcess",
			text: "Organisation",
			url: "#",
			icon: faUniversity.iconName,
			children: [{
				id: "eProcessOrganisationOrganisationStructure",
				parentId: "eProcessOrganisation",
				text: "Organiastion structure",
				url: "#",
				icon: faUniversity.iconName,
				children: null,
				isOpen: false,
				isSelected: false
			}, {
				id: "eProcessOrganisationOrganisationSettings",
				parentId: "eProcessOrganisation",
				text: "Organisation settings",
				url: "#",
				icon: faUniversity.iconName,
				children: null,
				isOpen: false,
				isSelected: false
			}],
			isOpen: false,
			isSelected: false
		}],
		isOpen: false,
		isSelected: false
	}, {
		id: "settings",
		parentId: null,
		text: "Settings",
		url: "#",
		icon: faCogs.iconName,
		children: [{
			id: "settingsCommon",
			parentId: "settings",
			text: "Common",
			url: "/settings/common",
			icon: faCog.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "settingsEmail",
			parentId: "settings",
			text: "Email",
			url: "#",
			icon: faMailBulk.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "settingsEncryption",
			parentId: "settings",
			text: "Encryption",
			url: "#",
			icon: faKey.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}, {
			id: "settingsRegistration",
			parentId: "settings",
			text: "Registration",
			url: "#",
			icon: faUser.iconName,
			children: null,
			isOpen: false,
			isSelected: false
		}],
		isOpen: false,
		isSelected: false
	}]

	private onClick(selectedItem: SidebarItem): void {
		this.onOpenClickParam(selectedItem, this.sidebarItems, true);
	}

	private onClickInner(selectedItem: SidebarItem): void {
		this.onOpenClickParam(selectedItem, this.sidebarItems, false);
	}

	private onOpenClickParam(selectedItem: SidebarItem, items: SidebarItem[], collapseParent: boolean): void {
		if (selectedItem.children) {
			if (collapseParent) {
				items.forEach((item, index) => {
					if (item.id !== selectedItem.id) {
						if (item.id !== selectedItem.parentId) {
							item.isOpen = false;
						}
					} else {
						item.isOpen = !item.isOpen;
					}

					if (item.children) {
						this.onOpenClickParam(selectedItem, item.children, collapseParent);
					}
				});
			}
		} else {
			items.forEach((item, index) => {
				item.isSelected = false;

				if (item.id !== selectedItem.parentId && collapseParent) {
					item.isOpen = false;
				}

				if (item.children) {
					this.onOpenClickParam(selectedItem, item.children, collapseParent);
				}
			});

			selectedItem.isSelected = true;
		}
	}
}

interface SidebarItem {
	id: string;
	parentId: string;
	text: string;
	url: string;
	icon: string;
	children: SidebarItem[];
	isOpen: boolean;
	isSelected: boolean;
}
