import React, { Suspense } from 'react';
import { Breadcrumb, Layout, theme } from 'antd';
import { useRoutes } from 'react-router';

import MenuBar from './component/MenuBar';
import { routes } from './routes/routes';

const { Header, Content, Footer } = Layout;

const App: React.FC = () => {
  const elements = useRoutes(routes);
  const {
    token: { colorBgContainer },
  } = theme.useToken();

  return (
    <Layout className="layout">
      <Header style={{ display: 'flex', alignItems: 'center' }}>
        <div className="demo-logo" />
        <MenuBar />
      </Header>
      <Content style={{ padding: '0 50px' }}>
        <Breadcrumb style={{ margin: '16px 0' }}>
          <Breadcrumb.Item>Home</Breadcrumb.Item>
          <Breadcrumb.Item>List</Breadcrumb.Item>
          <Breadcrumb.Item>App</Breadcrumb.Item>
        </Breadcrumb>

        {/* Routes */}
        <Content
          style={{
            padding: 24,
            margin: 0,
            background: colorBgContainer,
          }}
        >

          <Suspense fallback={<div>Loading...</div>}>
            {elements}
          </Suspense>
        </Content>
      </Content>
      <Footer style={{ textAlign: 'center' }}>Ant Design ©2023 Created by Ant UED</Footer>
    </Layout>
  );
};

export default App;