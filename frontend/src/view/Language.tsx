import React, { useEffect } from 'react';
import { Col, Row } from 'antd';
import IndexCard from '../component/IndexCard';

const Language: React.FC = () => {
  useEffect(() => {
    
  }, []);

  return (
    <Row justify="space-around">
      <Col span={4}>
        <IndexCard imgSrc='https://os.alipayobjects.com/rmsportal/QBnOOoLaAfKPirc.png' title='IELTS' description='International English Language Testing System' />
      </Col>
    </Row>
  )
}

export default Language;