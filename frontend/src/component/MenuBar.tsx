import { HomeOutlined } from '@ant-design/icons';
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
  const navigate = useNavigate();
  const menuClick = (menuInfo: { key: string }) => {
    navigate(menuInfo.key)
  }

  return (
    <Menu
      theme="dark"
      mode="horizontal"
      defaultSelectedKeys={['1']}
      items={[
        getItem("Home", "/home", <HomeOutlined />),
        getItem("Chinese", "/listening/chinese"),
        getItem("English", "/listening/english"),
        getItem("Login", "/login")
      ]}
      onClick={menuClick}
      style={{
        width: '100%',
        marginRight: 'auto'
      }}
    >
    </Menu>
  )
}

export default MenuBar;