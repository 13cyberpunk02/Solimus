export interface NavItem {
  label: string;
  route: string;
  icon: string;
  badge?: number | string;
}

export interface NavGroup {
  title?: string;
  items: NavItem[];
}