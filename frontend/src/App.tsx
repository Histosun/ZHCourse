import { useRoutes } from "react-router-dom";
import { Content, Header, Footer } from "antd/es/layout/layout";
import Routes from "./Routes";

function App() {
  const elements = useRoutes(Routes);
  return (
    <div className="App">
      <Header/>
      <Content style={{ padding: '0 50px' }}>
        {/* <Breadcrumb style={{ margin: '16px 0' }}>
          <Breadcrumb.Item>
            <NavLink to = "/Home">Home</NavLink>
          </Breadcrumb.Item>
          <Breadcrumb.Item>
            <NavLink to = "/Home/List">List</NavLink>
          </Breadcrumb.Item>
        </Breadcrumb> */}
        <div className="site-layout-content" style={{ marginTop: 20 }}>
          {elements}
        </div>
      </Content>
      <Footer style={{ textAlign: 'center' }}>Ant Design ©2018 Created by Ant UED</Footer>
    </div>
  );
}

export default App;
