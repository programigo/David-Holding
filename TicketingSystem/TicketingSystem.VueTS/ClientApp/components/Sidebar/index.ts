import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component({
	name: 'the-sidebar',
	components: {
	}
})

export default class Sidebar extends Vue {

	private sidebarItems: SidebarItem[] = [{
		id: "home",
		parentId: null,
		text: "Home",
		url: '/',
		icon: "",
		children: null,
		isOpen: false,
		isSelected: false
	}, {
		id: "projects",
		parentId: null,
		text: "Projects",
		url: "/projects",
		icon: "",
		children: null,
		isOpen: false,
		isSelected: false
	}
	]

	private onClick(selectedItem: SidebarItem): void {
		this.onOpenClickParam(selectedItem, this.sidebarItems, true);
	}

	private onClickInner(selectedItem: SidebarItem): void {
		this.onOpenClickParam(selectedItem, this.sidebarItems, false);
	}

	onOpenClickParam(selectedItem: SidebarItem, items: SidebarItem[], collapseParent: boolean): void {
		if (selectedItem.children) {
			if (collapseParent) {
				items.forEach((item, index) => {
					if (item.id == selectedItem.id) {
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
	icon: string,
	children: SidebarItem[];
	isOpen: boolean;
	isSelected: boolean;
}