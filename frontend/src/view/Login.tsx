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
  ProFormCaptcha,
  ProFormCheckbox,
  ProFormText,
} from '@ant-design/pro-components';
import { Space } from 'antd';
import { AnyRecord } from 'dns';
import type { CSSProperties } from 'react';

// customize form validator
const validators = {
  username(_: any, value: string) {
    value = value.trim();
    if(value.length == 0) return Promise.reject();
    if (value.length < 6) return Promise.reject();
    return Promise.resolve();
  },
  password(_: any, value: string) {
    value = value.trim();
    if(value.length == 0) return Promise.reject();
    if (value.length < 6) return Promise.reject();
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

export default () => {
  const submit = async (values: AnyRecord) => { alert(JSON.stringify(values)) }

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