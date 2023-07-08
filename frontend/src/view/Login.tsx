import {
  LockOutlined,
  GoogleOutlined,
  TaobaoCircleOutlined,
  UserOutlined,
  WeiboCircleOutlined,
} from '@ant-design/icons';
import {
  LoginForm,
  ProConfigProvider,
  ProFormCheckbox,
  ProFormText,
} from '@ant-design/pro-components';
import { Space } from 'antd';
import { useState, type CSSProperties } from 'react';
import api from '../api';
import { connect } from 'react-redux';
import action from '../store/action';
import { useNavigate } from 'react-router';
import util from '../util';

// customize form validator
const validators = {
  username(_: any, value: string) {
    value = value.trim();
    if (value.length === 0 || value.length < 5) return Promise.reject();
    return Promise.resolve();
  },
  password(_: any, value: string) {
    if (!value) return Promise.reject();
    value = value.trim();
    if (value.length === 0 || value.length < 6) return Promise.reject();
    return Promise.resolve();
  }
}

const iconStyles: CSSProperties = {
  marginInlineStart: '16px',
  color: 'rgba(0, 0, 0, 0.2)',
  fontSize: '24px',
  verticalAlign: 'middle',
  cursor: 'pointer',
};

const LoginView = (props: any) => {
  const [checked, setChecked] = useState(false);

  let navigate = useNavigate();
  let { login } = props;

  const submit = async (values: any) => {
    api.user.login({ username: values.username, password: values.password })
      .then(result => {
        let token = result.data as string;
        if (!token) {
          return;
        }

        if (checked) {
          util.cookie.setToken(token);
        }

        login(token);
        navigate(-1);
      })
      .catch((err) => {
        console.log(err);
      });
  }

  return (
    <ProConfigProvider hashed={false}>
      <div style={{ backgroundColor: 'white' }}>
        <LoginForm
          logo="https://github.githubassets.com/images/modules/logos_page/Octocat.png"
          title="ZHCourse"
          subTitle="A place for you to practice listening"
          actions={
            <Space>
              Login through
              <GoogleOutlined style={iconStyles} />
              <TaobaoCircleOutlined style={iconStyles} />
              <WeiboCircleOutlined style={iconStyles} />
            </Space>
          }
          submitter={{
            searchConfig: {
              submitText: "Login",
            }
          }}
          onFinish={submit}
        >
          <ProFormText
            name="username"
            fieldProps={{
              size: 'large',
              prefix: <UserOutlined className={'prefixIcon'} />,
            }}
            placeholder={'Username'}
            rules={[
              {
                validator: validators.username,
                message: 'Invalid username!'
              }
            ]}
          />
          <ProFormText.Password
            name="password"
            fieldProps={{
              size: 'large',
              prefix: <LockOutlined className={'prefixIcon'} />,
            }}
            placeholder={'Password'}
            rules={[
              {
                validator: validators.password,
                message: 'Invalid password!',
              }
            ]}
          />
          <div
            style={{
              marginBlockEnd: 24,
            }}
          >
            <ProFormCheckbox noStyle name="autoLogin">
              Remember me
            </ProFormCheckbox>
            <a
              style={{
                float: 'right',
              }}
            >
              Forget password?
            </a>
          </div>
        </LoginForm>
      </div>
    </ProConfigProvider>
  );
};

export default connect(null, action.user)(LoginView);