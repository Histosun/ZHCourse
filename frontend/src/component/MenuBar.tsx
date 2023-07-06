import { Menu, MenuProps } from 'antd';
import { useNavigate } from 'react-router';

type MenuItem = Required<MenuProps>['items'][number];

function getItem(
    label: React.ReactNode,
    key?: React.Key | null,
    icon?: React.ReactNode,
    children?: MenuItem[],
    theme?: 'light' | 'dark',
  ): MenuItem {
    return {
      key,
      icon,
      children,
      label,
      theme,
    } as MenuItem;
  }

const MenuBar: React.FC = () => {
  const nav = useNavigate();
  const menuClick = (menuInfo: {key: string}) => {
    nav(menuInfo.key)
  }

  return (
    <Menu
      theme="dark"
      mode="horizontal"
      defaultSelectedKeys={['1']}
      items={[
        getItem("Home", "/home"),
        getItem("Chinese", "/listening/chinese"),
        getItem("English", "/listening/english")
      ]}
      onClick={menuClick}
    />
  )
}

export default MenuBar;